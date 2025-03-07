using Kiemtra_web.Models;
using Microsoft.EntityFrameworkCore;

namespace Kiemtra_web.Data
{
    public class HanghoaContext : DbContext
    {
        public HanghoaContext(DbContextOptions<HanghoaContext> options) : base(options) { }
        public DbSet<Hang_hoa> Hanghoa { get; set; }
        

    }
}
