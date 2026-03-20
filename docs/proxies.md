# Proxy (Proxies)

Quản lý danh sách proxy trong GPMLogin Global.

---

## Lấy danh sách tất cả proxy

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/proxies</code></td></tr></tbody></table>

**Tham số query:**

| Tham số     | Kiểu    | Bắt buộc | Mô tả                                                                                     | Mặc định |
| ----------- | ------- | -------- | ----------------------------------------------------------------------------------------- | -------- |
| `page`      | integer | Không    | Số trang                                                                                  | `1`      |
| `page_size` | integer | Không    | Số lượng kết quả mỗi trang                                                                | `10`     |
| `search`    | string  | Không    | Từ khóa tìm kiếm                                                                          | `""`     |
| `sort`      | integer | Không    | Cách sắp xếp: `0` = mới nhất trước, `1` = cũ nhất trước, `2` = tên A-Z, `3` = tên Z-A   | `1`      |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/proxies?page=1&page_size=10&search=&sort=1
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "current_page": 1,
    "per_page": 10,
    "total": 10,
    "last_page": 1,
    "data": [
      {
        "id": "a36a1e31-bdae-4566-95df-f246996a141c",
        "raw_proxy": "14.244.137.125:24009:1:1",
        "meta_data": "{\"is_alive\":true,\"last_check\":\"2025-09-08T08:55:29.9649830Z\"}",
        "created_at": "2025-09-08T08:55:22.0361423",
        "updated_at": "2025-09-08T08:55:22.0361424",
        "tags": [
          {
            "id": "2c66f5c5-dade-4cf2-9df0-a79a7f02466a",
            "name": "live",
            "color": "Green",
            "category": "proxy"
          }
        ]
      }
    ]
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

**Mô tả các trường trả về:**

| Trường       | Kiểu   | Mô tả                                   |
| ------------ | ------ | --------------------------------------- |
| `id`         | string | UUID định danh proxy                    |
| `raw_proxy`  | string | Chuỗi proxy thô (IP:Port:User:Pass hoặc URL) |
| `meta_data`  | string | JSON chứa trạng thái kiểm tra (`is_alive`, `last_check`) |
| `created_at` | datetime | Thời điểm tạo                         |
| `updated_at` | datetime | Thời điểm cập nhật lần cuối           |
| `tags`       | array  | Danh sách tag gán cho proxy             |

---

## Lấy thông tin một proxy

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/proxies/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả           |
| ------- | -------- | --------------- |
| `id`    | Có       | UUID của proxy  |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/proxies/a36a1e31-bdae-4566-95df-f246996a141c
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": "a36a1e31-bdae-4566-95df-f246996a141c",
    "raw_proxy": "14.244.137.125:24009:1:1",
    "meta_data": "{\"is_alive\":true,\"last_check\":\"2025-09-08T08:55:29.9649830Z\"}",
    "created_at": "2025-09-08T08:55:22.0361423",
    "updated_at": "2025-09-08T08:55:22.0361424",
    "tags": []
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Tạo proxy mới

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>POST</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/proxies/create</code></td></tr><tr><td><strong>Content-Type</strong></td><td><code>application/json</code></td></tr></tbody></table>

**Request body:**

```json
{
  "raw_proxy": "socks5://127.0.0.1:5001"
}
```

| Trường      | Kiểu   | Bắt buộc | Mô tả                                             |
| ----------- | ------ | -------- | ------------------------------------------------- |
| `raw_proxy` | string | Có       | Chuỗi proxy. Hỗ trợ các định dạng: `IP:Port`, `IP:Port:User:Pass`, `socks5://...`, `http://...` |

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": "20c90b14-7b53-4e13-a302-8f2ce31bbeed",
    "raw_proxy": "socks5://127.0.0.1:5001",
    "meta_data": null,
    "created_at": "2025-09-08T15:11:50.1867076Z",
    "updated_at": "2025-09-08T15:11:50.1867081Z",
    "tags": []
  },
  "message": "ProxyAdded",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Cập nhật proxy

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>POST</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/proxies/update/{id}</code></td></tr><tr><td><strong>Content-Type</strong></td><td><code>application/json</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả          |
| ------- | -------- | -------------- |
| `id`    | Có       | UUID của proxy |

**Request body:**

```json
{
  "raw_proxy": "socks5://127.0.0.1:5001"
}
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": "20c90b14-7b53-4e13-a302-8f2ce31bbeed",
    "raw_proxy": "socks5://127.0.0.1:5001",
    "meta_data": null,
    "created_at": "2025-09-08T15:11:50.1867076",
    "updated_at": "2025-09-08T15:11:50.1867081",
    "tags": []
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Xóa proxy

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/proxies/delete/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả          |
| ------- | -------- | -------------- |
| `id`    | Có       | UUID của proxy |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/proxies/delete/20c90b14-7b53-4e13-a302-8f2ce31bbeed
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": null,
  "message": "ProxyDeleted",
  "sender": "GPMLoginGlobal v1.0.0"
}
```
