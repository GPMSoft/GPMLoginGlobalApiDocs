# Nhóm (Groups)

Quản lý các nhóm dùng để phân loại profile trong GPMLogin Global.

---

## Lấy danh sách tất cả nhóm

<table data-full-width="false"><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/groups</code></td></tr></tbody></table>

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/groups
```

**Ví dụ phản hồi (200 OK):**

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
        "id": "4fd6db25-12cc-4ba2-b943-b7583db807fc",
        "name": "test 1",
        "order": 2,
        "created_at": "2025-09-08T10:18:19.5808258",
        "updated_at": "2025-09-08T10:18:19.580826"
      },
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

**Mô tả các trường trả về:**

| Trường         | Kiểu     | Mô tả                          |
| -------------- | -------- | ------------------------------ |
| `id`           | string   | UUID định danh nhóm            |
| `name`         | string   | Tên nhóm                       |
| `order`        | integer  | Thứ tự hiển thị (số nhỏ = trên)|
| `created_at`   | datetime | Thời điểm tạo                  |
| `updated_at`   | datetime | Thời điểm cập nhật lần cuối    |

---

## Lấy thông tin một nhóm

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/groups/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả            |
| ------- | -------- | ---------------- |
| `id`    | Có       | UUID của nhóm    |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/groups/67db46f9-df8c-41c5-a18f-631a090a17a1
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": "67db46f9-df8c-41c5-a18f-631a090a17a1",
    "name": "Default group",
    "order": 0,
    "created_at": "2025-09-04T08:20:39.7115442",
    "updated_at": "2025-09-04T08:20:39.7115442"
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Tạo nhóm mới

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>POST</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/groups/create</code></td></tr><tr><td><strong>Content-Type</strong></td><td><code>application/json</code></td></tr></tbody></table>

**Request body:**

```json
{
  "name": "Group created from api",
  "order": 999
}
```

| Trường  | Kiểu    | Bắt buộc | Mô tả                           |
| ------- | ------- | -------- | ------------------------------- |
| `name`  | string  | Có       | Tên nhóm                        |
| `order` | integer | Không    | Thứ tự hiển thị (mặc định: 999) |

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": "926ecb7b-7f5d-4191-9568-0fe1524b18f4",
    "name": "Group created from api",
    "order": 999,
    "created_at": "2025-09-08T15:04:30.6740464Z",
    "updated_at": "2025-09-08T15:04:30.6740465Z"
  },
  "message": "GroupAdded",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Cập nhật nhóm

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>POST</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/groups/update/{id}</code></td></tr><tr><td><strong>Content-Type</strong></td><td><code>application/json</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả         |
| ------- | -------- | ------------- |
| `id`    | Có       | UUID của nhóm |

**Request body:**

```json
{
  "name": "Group edited by api",
  "order": 999
}
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": "926ecb7b-7f5d-4191-9568-0fe1524b18f4",
    "name": "Group edited by api",
    "order": 999,
    "created_at": "2025-09-08T15:04:30.6740464",
    "updated_at": "2025-09-08T15:04:30.6740465"
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Xóa nhóm

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/groups/delete/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả         |
| ------- | -------- | ------------- |
| `id`    | Có       | UUID của nhóm |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/groups/delete/926ecb7b-7f5d-4191-9568-0fe1524b18f4
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": null,
  "message": "GROUP_DELETED",
  "sender": "GPMLoginGlobal v1.0.0"
}
```
