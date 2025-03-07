const API_URL = "https://localhost:7171/api/HangHoa";

// Load dữ liệu từ API
async function loadGoods() {
  const response = await fetch(API_URL);
  const goods = await response.json();

  let table = document.getElementById("goodsTable");
  table.innerHTML = "";

  goods.forEach((good) => {
    let row = `<tr>
            <td>${good.MaHangHoa}</td>
            <td>${good.TenHangHoa}</td>
            <td>${good.SoLuong}</td>
            <td>${good.ghi_chu || ""}</td>
            <td>
                <button onclick="editGood('${good.MaHangHoa}', '${
      good.TenHangHoa
    }', ${good.SoLuong}, '${good.ghi_chu || ""}')">Sửa</button>
                <button onclick="deleteGood('${good.MaHangHoa}')">Xóa</button>
            </td>
        </tr>`;
    table.innerHTML += row;
  });
}

// Thêm hoặc cập nhật hàng hóa
async function addOrUpdateHangHoa() {
  let MaHangHoa = document.getElementById("MaHangHoa").value;
  let TenHangHoa = document.getElementById("TenHangHoa").value;
  let SoLuong = document.getElementById("SoLuong").value;
  let ghi_chu = document.getElementById("ghi_chu").value;

  const response = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ MaHangHoa, TenHangHoa, SoLuong, ghi_chu }),
  });

  if (response.ok) {
    alert("Thêm/Cập nhật thành công!");
    loadGoods();
  } else {
    alert("Lỗi khi lưu dữ liệu!");
  }
}

// Xóa hàng hóa
async function deleteGood(MaHangHoa) {
  const response = await fetch(`${API_URL}/${MaHangHoa}`, { method: "DELETE" });

  if (response.ok) {
    alert("Xóa thành công!");
    loadGoods();
  } else {
    alert("Lỗi khi xóa!");
  }
}

// Cập nhật ghi chú
async function updateGhiChu(MaHangHoa) {
  let newGhiChu = prompt("Nhập ghi chú mới:");
  if (!newGhiChu) return;

  const response = await fetch(`${API_URL}/${MaHangHoa}/update-ghi-chu`, {
    method: "PATCH",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newGhiChu),
  });

  if (response.ok) {
    alert("Ghi chú đã được cập nhật!");
    loadGoods();
  } else {
    alert("Lỗi khi cập nhật ghi chú!");
  }
}

// Chỉnh sửa hàng hóa
function editGood(MaHangHoa, TenHangHoa, SoLuong, ghi_chu) {
  document.getElementById("MaHangHoa").value = MaHangHoa;
  document.getElementById("TenHangHoa").value = TenHangHoa;
  document.getElementById("SoLuong").value = SoLuong;
  document.getElementById("ghi_chu").value = ghi_chu;
}

// Load dữ liệu khi trang web tải
loadGoods();
