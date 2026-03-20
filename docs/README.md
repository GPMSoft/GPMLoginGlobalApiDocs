# GPMLogin Global – Tài liệu API

**GPMLogin Global Local API** cho phép bạn kết nối và điều khiển GPMLogin Global trực tiếp qua HTTP mà không cần thao tác qua giao diện người dùng.

---

## Thông tin chung

| Thuộc tính       | Giá trị                          |
| ---------------- | -------------------------------- |
| **Phiên bản API**| v1                               |
| **Base URL**     | `http://{{Local URL}}/api/v1`    |
| **Giao thức**    | HTTP                             |
| **Định dạng**    | JSON                             |

> `{{Local URL}}` là địa chỉ máy chủ cục bộ nơi GPMLogin Global đang chạy (ví dụ: `http://127.0.0.1:9495`).

---

## Cấu trúc phản hồi

Tất cả các API đều trả về JSON theo định dạng chuẩn sau:

```json
{
  "success": true,
  "data": { ... },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

| Trường    | Kiểu dữ liệu | Mô tả                                      |
| --------- | ------------ | ------------------------------------------ |
| `success` | boolean      | `true` nếu thành công, `false` nếu thất bại |
| `data`    | object/array/null | Dữ liệu trả về (có thể là `null`)      |
| `message` | string       | Thông báo kết quả                          |
| `sender`  | string       | Phiên bản ứng dụng                         |

---

## Danh sách nhóm API

| Nhóm                                           | Mô tả                                      |
| ---------------------------------------------- | ------------------------------------------ |
| [Nhóm (Groups)](api/groups.md)                 | Quản lý các nhóm profile                  |
| [Proxy (Proxies)](api/proxies.md)              | Quản lý danh sách proxy                    |
| [Hồ sơ trình duyệt (Profiles)](api/profiles.md) | Tạo, sửa, xóa và điều khiển profile     |
| [Tiện ích mở rộng (Extensions)](api/extensions.md) | Quản lý extension cài đặt trong GPM   |
