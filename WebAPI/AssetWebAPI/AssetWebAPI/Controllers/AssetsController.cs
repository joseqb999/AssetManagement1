using AssetWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetWebAPI.DTO;
using AutoMapper;
using AssetWebAPI.AutoMapper;
using AssetWebAPI.Helpers;
using Microsoft.AspNetCore.Hosting;
using Fingers10.ExcelExport.ActionResults;

namespace AssetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {

        private readonly MyAssetsDBContext _context;
        private readonly IMapper mapper;
        public AssetsController(MyAssetsDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListDTO>>> GetAssets()
        {
            var query = from t1 in _context.Assets
                        join t2 in _context.Models on t1.ModelId equals t2.ModelId
                        join t3 in _context.Manufacturers on t1.ManuFactId equals t3.ManuFactId
                        join t4 in _context.Colors on t1.ColorId equals t4.ColorId
                        select new ListDTO
                        {
                            Id = t1.Id,
                            Name = t1.Name,
                            ManufacturerName = t3.Name,
                            ModelName = t2.Name,
                            ModelId = t1.ModelId,
                            ManuFactId = t1.ManuFactId,
                            ColorId = t1.ColorId,
                            ColorName = t4.Name,
                            Price = t1.Price,
                            InUse = t1.InUse,
                            Description = t1.Description,
                            PurchaseDate = t1.PurchaseDate
                        };


            var joinresult = await query.ToListAsync();

            if (joinresult == null)
            {
                return NotFound();
            }
            var result = new List<ListDTO>();
            result = mapper.Map<List<ListDTO>>(joinresult);
       
            return result;
        }

        [HttpGet("{AssetId}")]
        public async Task<ActionResult<List<ListDTO>>> GetAsset(Guid AssetId)
        {

            var query = from t1 in _context.Assets
                        where t1.Id == AssetId
                        join t2 in _context.Models on t1.ModelId equals t2.ModelId
                        join t3 in _context.Manufacturers on t1.ManuFactId equals t3.ManuFactId
                        join t4 in _context.Colors on t1.ColorId equals t4.ColorId
                        

            select new ListDTO
                        {
                            Id = t1.Id,
                            Name = t1.Name,
                            ModelId = t1.ModelId,
                            ManuFactId = t1.ManuFactId,
                            ColorId = t1.ColorId,
                            ManufacturerName = t3.Name,
                            ModelName = t2.Name,
                            ColorName = t4.Name,
                            Price = t1.Price,
                            InUse = t1.InUse,
                            Description = t1.Description,
                            PurchaseDate =t1.PurchaseDate
                        };
            
            var joinresult = await query.ToListAsync();

            if (joinresult == null)
            {
                return NotFound();
            }
            var result = new List<ListDTO>();
            result = mapper.Map<List<ListDTO>>(joinresult);
            return result;
        }


        [HttpGet("filter")]
        public async Task<ActionResult<List<ListDTO>>> Filter([FromQuery] PaginationDTO pagination)
        {

            var queryable = _context.Assets.AsQueryable();
          
            if (!(pagination.ManuFactId.Equals(Guid.Empty)))
            {
                queryable = queryable.Where(x => x.ManuFactId.Equals(pagination.ManuFactId));
            }

            if (!(pagination.ModelId.Equals(Guid.Empty)))
            {
                queryable = queryable.Where(x => x.ModelId.Equals(pagination.ModelId));
            }

            if (!string.IsNullOrWhiteSpace(pagination.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(pagination.Name));
            }


            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var assets = await queryable.Paginate(pagination)
                .Include(x => x.Manufacturer)
                .Include(x => x.Model)
                .Include(x => x.Color)
                .ToListAsync();
            return mapper.Map<List<ListDTO>>(assets);
        }

        [HttpGet("totalRecords")]
       
            public async Task<int> Count()
            {
                var query = from assets in this._context.Assets
                            select assets.Id;
                return await query.CountAsync();
            }
      

        [HttpGet("manufacturers")]
        public async Task<ActionResult<List<Manufacturer>>> GetManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        [HttpGet("models")]
        public async Task<ActionResult<List<Model>>> GetModels()
        {
            return await _context.Models.ToListAsync();
        }

        [HttpGet("colors")]
        public async Task<ActionResult<List<Color>>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AssetInsertDTO assetInsert)
        {



            Asset assets = new Asset()
            {
                Id = Guid.NewGuid(),
                Name = assetInsert.Name,
                ManuFactId = assetInsert.ManuFactId,
                ModelId = assetInsert.ModelId,
                ColorId = assetInsert.ColorId,
                Price = assetInsert.Price,
                InUse = assetInsert.InUse,
                PurchaseDate = assetInsert.PurchaseDate,
                Description = assetInsert.Description,
                CreatedDateTime = DateTime.Now
        };

        
            _context.Assets.Add(assets);
            await _context.SaveChangesAsync();

            
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Put([FromBody] AssetInsertDTO assetInsert)
        {

            Asset assets = new Asset()
            {
                Id = assetInsert.Id,
                Name = assetInsert.Name,
                ManuFactId = assetInsert.ManuFactId,
                ModelId = assetInsert.ModelId,
                ColorId = assetInsert.ColorId,
                Price = assetInsert.Price,
                InUse = assetInsert.InUse,
                PurchaseDate = assetInsert.PurchaseDate,
                Description = assetInsert.Description,
                CreatedDateTime = DateTime.Now

            };
            _context.Entry(assets).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return NoContent();
        }


    }
}
