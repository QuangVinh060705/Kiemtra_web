using Kiemtra_web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kiemtra_web.Controllers
{
    public class HangHoaMVCController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor để inject HttpClient
        public HangHoaMVCController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: /HangHoaMVC
        public async Task<IActionResult> Index()
        {
            // Gọi API để lấy danh sách hàng hóa
            var response = await _httpClient.GetStringAsync("https://localhost:7171/api/HangHoa"); // Đảm bảo URL chính xác cho API của bạn
            var hangHoas = JsonConvert.DeserializeObject<List<Hang_hoa>>(response);

            // Trả về View với dữ liệu
            return View(hangHoas);
        }

        // GET: /HangHoaMVC/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            // Gọi API để lấy chi tiết hàng hóa
            var response = await _httpClient.GetStringAsync($"https://localhost:7171/api/HangHoa/{id}");
            var hangHoa = JsonConvert.DeserializeObject<Hang_hoa>(response);

            if (hangHoa == null)
            {
                return NotFound();
            }

            // Trả về View chi tiết với dữ liệu
            return View(hangHoa);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hang_hoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                var hangHoaJson = JsonConvert.SerializeObject(hangHoa);
                var content = new StringContent(hangHoaJson, Encoding.UTF8, "application/json");

                try
                {
                    var response = await _httpClient.PostAsync("https://localhost:7171/api/HangHoa", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Sau khi thêm hàng hóa thành công, chuyển hướng về trang danh sách
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error: " + errorMessage); // Log lỗi từ API
                        return BadRequest(errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message); // Log exception nếu có
                    return BadRequest("Lỗi khi thêm hàng hóa");
                }
            }

            return View(hangHoa); // Trả về lại view nếu có lỗi
        }



        [HttpPost]
        public async Task<IActionResult> Edit(string id, Hang_hoa hangHoa)
        {
            if (id != hangHoa.MaHangHoa)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Gửi yêu cầu PUT đến API để cập nhật hàng hóa
                var hangHoaJson = JsonConvert.SerializeObject(hangHoa);
                var content = new StringContent(hangHoaJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"https://localhost:7171/api/HangHoa/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Sau khi cập nhật thành công, chuyển hướng đến trang chi tiết
                    return RedirectToAction(nameof(Details), new { id = id });
                }
                return BadRequest();
            }

            return View(hangHoa); // Trả lại form nếu có lỗi
        }


        // POST: /HangHoaMVC/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            // Gửi yêu cầu DELETE đến API để xóa hàng hóa
            var response = await _httpClient.DeleteAsync($"https://localhost:7171/api/HangHoa/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Sau khi xóa thành công, quay lại trang danh sách hàng hóa
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        // Cập nhật ghi chú hàng hóa (PATCH)
        [HttpPost]
        public async Task<IActionResult> UpdateGhiChu(string id, string ghiChu)
        {
            // Gửi yêu cầu PATCH đến API để cập nhật ghi chú cho hàng hóa
            var response = await _httpClient.PatchAsync(
                $"https://localhost:7171/api/HangHoa/{id}/update-ghi-chu",
                new StringContent(ghiChu, Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id = id });
            }

            return BadRequest();
        }
    }
}
