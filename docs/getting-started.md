# Bắt đầu nhanh

## Yêu cầu

* GPMLogin Global đang chạy trên máy tính
* Biết địa chỉ và cổng của Local API (mặc định: `http://127.0.0.1:9495`)

---

## Cách lấy Base URL

Mở GPMLogin Global → vào phần **Cài đặt** → tìm mục **Local API** để lấy địa chỉ và cổng đang lắng nghe.

---

## Ví dụ gọi API đầu tiên

Lấy danh sách tất cả nhóm:

```bash
curl http://127.0.0.1:19995/api/v1/groups
```

Kết quả mẫu:

```json
{
  "success": true,
  "data": {
    "current_page": 1,
    "per_page": 30,
    "total": 2,
    "last_page": 1,
    "data": [
      {
        "id": "67db46f9-df8c-41c5-a18f-631a090a17a1",
        "name": "Default group",
        "order": 0,
        "created_at": "2025-09-04T08:20:39.7115442",
        "updated_at": "2025-09-04T08:20:39.7115442"
      }
    ]
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Quy trình làm việc điển hình

```
1. Lấy danh sách Groups  →  GET /api/v1/groups
2. Tạo Profile mới       →  POST /api/v1/profiles/create
3. Khởi động trình duyệt →  GET /api/v1/profiles/start/{id}
4. Thao tác tự động      →  (dùng Remote Debugging Port trả về)
5. Dừng trình duyệt      →  GET /api/v1/profiles/stop/{id}
```

---

## Headers

Các API hiện tại **không yêu cầu xác thực** (authentication). Chỉ cần đảm bảo gửi `Content-Type: application/json` với các request có body.

```http
Content-Type: application/json
```
