using System;
using Kiemtra_web.Data;
using Kiemtra_web.Models;
using Kiemtra_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HangHoaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly HanghoaContext _context;

        public HangHoaController(HanghoaContext context)
        {
            _context = context;
        }

        // GET: api/HangHoa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hang_hoa>>> GetHangHoas()
        {
            return await _context.Hanghoa.ToListAsync();
        }

        // GET: api/HangHoa/5 hoặc api/HangHoa?id=5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hang_hoa>> GetHangHoa(string id)
        {
            var hangHoa = await _context.Hanghoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return hangHoa;
        }

        [HttpGet("by-query")]
        public async Task<ActionResult<Hang_hoa>> GetHangHoaByQuery([FromQuery] string id)
        {
            var hangHoa = await _context.Hanghoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return hangHoa;
        }

        // POST: api/HangHoa (Thêm hàng hóa)
        [HttpPost]
        public async Task<ActionResult<Hang_hoa>> PostHangHoa(Hang_hoa hangHoa)
        {
            _context.Hanghoa.Add(hangHoa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHangHoa), new { id = hangHoa.MaHangHoa }, hangHoa);
        }

        // PUT: api/HangHoa/5 (Cập nhật hàng hóa)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHangHoa(string id, Hang_hoa hangHoa)
        {
            if (id != hangHoa.MaHangHoa)
            {
                return BadRequest();
            }

            _context.Entry(hangHoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Hanghoa.Any(e => e.MaHangHoa == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/HangHoa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHangHoa(string id)
        {
            var hangHoa = await _context.Hanghoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            _context.Hanghoa.Remove(hangHoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/update-ghi-chu")]
        public async Task<IActionResult> UpdateGhiChu(string id, [FromBody] string ghiChu)
        {
            var Hanghoa = await _context.Hanghoa.FindAsync(id);
            if (Hanghoa == null)
            {
                return NotFound();
            }

            Hanghoa.ghi_chu = ghiChu;
            await _context.SaveChangesAsync();

            return Ok(Hanghoa);
        }
    }
}
