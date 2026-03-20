# Tiện ích mở rộng (Extensions)

Quản lý các extension (tiện ích mở rộng) đã cài đặt trong GPMLogin Global.

---

## Lấy danh sách tất cả extension

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/extensions</code></td></tr></tbody></table>

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/extensions
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": "9d30b2b55768f1051833465850725851",
      "name": "Convert WebP to PNG / JPG",
      "version": "3.0.11",
      "is_active": false
    }
  ],
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

**Mô tả các trường trả về:**

| Trường      | Kiểu    | Mô tả                                           |
| ----------- | ------- | ----------------------------------------------- |
| `id`        | string  | ID định danh extension (Chrome Extension ID)    |
| `name`      | string  | Tên extension                                   |
| `version`   | string  | Phiên bản extension                             |
| `is_active` | boolean | `true` = đang bật, `false` = đang tắt           |

---

## Bật / Tắt extension

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/extensions/update-state/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả                                  |
| ------- | -------- | -------------------------------------- |
| `id`    | Có       | ID của extension (Chrome Extension ID) |

**Tham số query:**

| Tham số  | Kiểu    | Bắt buộc | Mô tả                                  |
| -------- | ------- | -------- | -------------------------------------- |
| `active` | boolean | Có       | `true` = bật extension, `false` = tắt  |

**Ví dụ request (tắt extension):**

```http
GET {{Local URL}}/api/v1/extensions/update-state/9d30b2b55768f1051833465850725851?active=false
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": null,
  "message": "Extension state updated successfully",
  "sender": "GPMLoginGlobal v1.0.0"
}
```
