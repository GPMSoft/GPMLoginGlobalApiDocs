# Hồ sơ trình duyệt (Profiles)

Quản lý và điều khiển các hồ sơ trình duyệt (browser profile) trong GPMLogin Global.

---

## Lấy danh sách tất cả profile

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/profiles</code></td></tr></tbody></table>

**Tham số query:**

| Tham số    | Kiểu    | Bắt buộc | Mô tả                                                                                   | Mặc định |
| ---------- | ------- | -------- | --------------------------------------------------------------------------------------- | -------- |
| `page`     | integer | Không    | Số trang                                                                                | `1`      |
| `per_page` | integer | Không    | Số lượng kết quả mỗi trang                                                              | `30`     |
| `search`   | string  | Không    | Từ khóa tìm kiếm theo tên profile                                                       | `""`     |
| `sort`     | integer | Không    | Cách sắp xếp: `0` = mới nhất trước, `1` = cũ nhất trước, `2` = tên A-Z, `3` = tên Z-A | `0`      |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/profiles?page=1&per_page=30&search=&sort=0
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "current_page": 1,
    "per_page": 30,
    "total": 15,
    "last_page": 1,
    "data": [
      {
        "id": "37f783ac-2635-4d53-ab8d-a300c790ecdc",
        "name": "Profile 4099",
        "group_id": "67db46f9-df8c-41c5-a18f-631a090a17a1",
        "storage_path": "37f783ac-2635-4d53-ab8d-a300c790ecdc",
        "raw_proxy": "",
        "browser": {
          "name": "chrome",
          "version": "139.0.7258.139"
        },
        "os": "windows",
        "note": "",
        "created_at": "2025-09-08 14:45:49",
        "updated_at": "2025-09-08 14:45:49",
        "tags": []
      }
    ]
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

**Mô tả các trường trả về:**

| Trường         | Kiểu   | Mô tả                                        |
| -------------- | ------ | -------------------------------------------- |
| `id`           | string | UUID định danh profile                       |
| `name`         | string | Tên profile                                  |
| `group_id`     | string | UUID nhóm mà profile thuộc về                |
| `storage_path` | string | Đường dẫn lưu trữ dữ liệu (thường = `id`)   |
| `raw_proxy`    | string | Proxy gán cho profile (chuỗi rỗng = không có)|
| `browser`      | object | Thông tin trình duyệt (`name`, `version`)    |
| `os`           | string | Hệ điều hành giả lập (`windows`, `macos`,…) |
| `note`         | string | Ghi chú                                      |
| `tags`         | array  | Danh sách tag gán cho profile                |

---

## Lấy thông tin một profile

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/profiles/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả            |
| ------- | -------- | ---------------- |
| `id`    | Có       | UUID của profile |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/profiles/37f783ac-2635-4d53-ab8d-a300c790ecdc
```

> Phản hồi trả về đầy đủ thông tin bao gồm cả đối tượng `fingerprint` chi tiết (navigator, WebGL, canvas, audio, v.v.).

---

## Tạo profile mới

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>POST</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/profiles/create</code></td></tr><tr><td><strong>Content-Type</strong></td><td><code>application/json</code></td></tr></tbody></table>

**Request body:**

```json
{
  "name": "Test profile from api",
  "group_id": null,
  "raw_proxy": "socks5://127.0.0.1:5000",
  "browser_type": 1,
  "browser_version": "137.0.7151.41",
  "os_type": 1,
  "custom_user_agent": null,
  "task_bar_title": "abc",
  "webrtc_mode": null,
  "fixed_webrtc_public_ip": "",
  "geolocation_mode": null,
  "canvas_mode": null,
  "client_rect_mode": null,
  "webgl_image_mode": null,
  "webgl_metadata_mode": null,
  "audio_mode": null,
  "font_mode": null,
  "timezone_base_on_ip": true,
  "timezone": null,
  "is_language_base_on_ip": true,
  "fixed_language": null
}
```

**Mô tả các trường:**

| Trường                  | Kiểu    | Bắt buộc | Mô tả                                                                                   |
| ----------------------- | ------- | -------- | --------------------------------------------------------------------------------------- |
| `name`                  | string  | Có       | Tên profile                                                                             |
| `group_id`              | string  | Không    | UUID nhóm. `null` = nhóm mặc định                                                       |
| `raw_proxy`             | string  | Không    | Chuỗi proxy. `null` hoặc `""` = không dùng proxy                                       |
| `browser_type`          | integer | Có       | Loại trình duyệt: `1` = Chrome, `2` = Firefox                                          |
| `browser_version`       | string  | Có       | Phiên bản trình duyệt đã cài (phải tồn tại trong GPM)                                  |
| `os_type`               | integer | Có       | Hệ điều hành: `1` = Windows, `2` = macOS Intel, `3` = macOS ARM, `4` = Linux, `5` = Android |
| `custom_user_agent`     | string  | Không    | User-Agent tùy chỉnh. `null` = tự động                                                  |
| `task_bar_title`        | string  | Không    | Tiêu đề hiển thị trên thanh taskbar                                                    |
| `webrtc_mode`           | integer | Không    | `null` = theo cài đặt mặc định, `1` = Theo IP, `2` = Cố định, `3` = Thực, `4` = Tắt  |
| `fixed_webrtc_public_ip`| string  | Không    | Bắt buộc khi `webrtc_mode = 2`                                                          |
| `geolocation_mode`      | integer | Không    | `null` = mặc định, `1` = Cho phép, `2` = Hỏi, `3` = Chặn                              |
| `canvas_mode`           | integer | Không    | `null` = mặc định, `1` = Nhiễu, `2` = Thực, `3` = Chặn                                |
| `client_rect_mode`      | integer | Không    | `null` = mặc định, `1` = Nhiễu, `2` = Thực                                             |
| `webgl_image_mode`      | integer | Không    | `null` = mặc định, `1` = Nhiễu, `2` = Thực                                             |
| `webgl_metadata_mode`   | integer | Không    | `null` = mặc định, `1` = Ẩn, `2` = Thực                                                |
| `audio_mode`            | integer | Không    | `null` = mặc định, `1` = Nhiễu, `2` = Thực                                             |
| `font_mode`             | integer | Không    | `null` = mặc định, `1` = Ẩn, `2` = Thực                                                |
| `timezone_base_on_ip`   | boolean | Không    | `true` = tự động theo IP, `false` = dùng `timezone`                                    |
| `timezone`              | string  | Không    | Múi giờ cụ thể (khi `timezone_base_on_ip = false`)                                     |
| `is_language_base_on_ip`| boolean | Không    | `true` = tự động theo IP, `false` = dùng `fixed_language`                              |
| `fixed_language`        | string  | Không    | Ngôn ngữ cố định, ví dụ: `vi`, `en`, `zh` (khi `is_language_base_on_ip = false`)       |

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": "69911f98-ffde-475d-aa91-fbb1c713937c",
    "name": "Test profile from api",
    "group_id": "67db46f9-df8c-41c5-a18f-631a090a17a1",
    "storage_path": "69911f98-ffde-475d-aa91-fbb1c713937c",
    "raw_proxy": "socks5://127.0.0.1:5000",
    "browser": { "name": "chrome", "version": "137.0.7151.41" },
    "os": "windows",
    "note": "",
    "created_at": "2025-09-08 15:19:25",
    "updated_at": "2025-09-08 15:19:25",
    "tags": [],
    "fingerprint": { "...": "..." }
  },
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

---

## Cập nhật profile

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>POST</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/profiles/update/{id}</code></td></tr><tr><td><strong>Content-Type</strong></td><td><code>application/json</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả            |
| ------- | -------- | ---------------- |
| `id`    | Có       | UUID của profile |

**Request body:** Tương tự [Tạo profile mới](#tạo-profile-mới).

---

## Xóa profile

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/profiles/delete/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả            |
| ------- | -------- | ---------------- |
| `id`    | Có       | UUID của profile |

**Tham số query:**

| Tham số | Kiểu   | Bắt buộc | Mô tả                                                              | Mặc định |
| ------- | ------ | -------- | ------------------------------------------------------------------ | -------- |
| `mode`  | string | Không    | `soft` = xóa mềm (có thể khôi phục), `hard` = xóa cứng (xóa cả dữ liệu lưu trữ) | `soft` |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/profiles/delete/69911f98-ffde-475d-aa91-fbb1c713937c?mode=hard
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": null,
  "message": "",
  "sender": "GPMLoginGlobal v1.0.0"
}
```

> ⚠️ **Cảnh báo:** Chế độ `hard` sẽ xóa toàn bộ dữ liệu lưu trữ của profile và **không thể khôi phục**.

---

## Khởi động trình duyệt

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/profiles/start/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả            |
| ------- | -------- | ---------------- |
| `id`    | Có       | UUID của profile |

**Tham số query:**

| Tham số                | Kiểu    | Bắt buộc | Mô tả                                                         | Mặc định |
| ---------------------- | ------- | -------- | ------------------------------------------------------------- | -------- |
| `remote_debugging_port`| integer | Không    | Cổng Remote Debugging Protocol (CDP). `0` = tự động chọn     | `0`      |
| `window_scale`         | float   | Không    | Tỷ lệ phóng to/thu nhỏ cửa sổ (ví dụ: `0.8` = 80%)          | `1`      |
| `window_pos`           | string  | Không    | Vị trí cửa sổ theo định dạng `x,y` (ví dụ: `100,100`)        | –        |
| `window_size`          | string  | Không    | Kích thước cửa sổ theo định dạng `width,height` (ví dụ: `800,600`) | –   |
| `addition_args`        | string  | Không    | Tham số dòng lệnh bổ sung truyền vào trình duyệt              | `""`     |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/profiles/start/7798d4ca-a002-4a52-9223-5140c68667bc?remote_debugging_port=40444&window_scale=0.8&window_pos=100,100&window_size=800,600&addition_args=
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": {
    "profile_id": "7798d4ca-a002-4a52-9223-5140c68667bc",
    "driver_path": "C:\\Users\\...\\gpmdriver.exe",
    "remote_debugging_port": 40444,
    "addition_info": {
      "process_id": 35632,
      "profile_name": "Profile 7221",
      "window_handle": 0
    }
  },
  "message": "",
  "sender": "GPMLoginGlobal v0.1.7-beta"
}
```

**Mô tả các trường trả về:**

| Trường                           | Kiểu    | Mô tả                                                   |
| -------------------------------- | ------- | ------------------------------------------------------- |
| `profile_id`                     | string  | UUID của profile đã khởi động                           |
| `driver_path`                    | string  | Đường dẫn đến `gpmdriver.exe` để dùng với Selenium/CDP  |
| `remote_debugging_port`          | integer | Cổng CDP để kết nối automation (Puppeteer, Playwright,…)|
| `addition_info.process_id`       | integer | PID của tiến trình trình duyệt                          |
| `addition_info.profile_name`     | string  | Tên profile                                             |
| `addition_info.window_handle`    | integer | Handle cửa sổ Windows (0 nếu không áp dụng)            |

> **Tip:** Sau khi khởi động, dùng `remote_debugging_port` để kết nối trình duyệt qua Puppeteer, Playwright hoặc Selenium với CDP.

---

## Dừng trình duyệt

<table><thead><tr><th>Thuộc tính</th><th>Giá trị</th></tr></thead><tbody><tr><td><strong>Method</strong></td><td><code>GET</code></td></tr><tr><td><strong>Endpoint</strong></td><td><code>/api/v1/profiles/stop/{id}</code></td></tr></tbody></table>

**Tham số đường dẫn:**

| Tham số | Bắt buộc | Mô tả            |
| ------- | -------- | ---------------- |
| `id`    | Có       | UUID của profile |

**Ví dụ request:**

```http
GET {{Local URL}}/api/v1/profiles/stop/37f783ac-2635-4d53-ab8d-a300c790ecdc
```

**Ví dụ phản hồi (200 OK):**

```json
{
  "success": true,
  "data": null,
  "message": "OK",
  "sender": "GPMLoginGlobal v1.0.0"
}
```
