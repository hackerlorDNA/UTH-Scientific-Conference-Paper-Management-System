# UTH-ConfMS - Conference Paper Management System
## Hệ Thống Quản Lý Giấy Tờ Hội Nghị Nghiên Cứu Khoa Học UTH

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React-18-61dafb)](https://reactjs.org/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-336791)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-7-dc382d)](https://redis.io/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ed)](https://www.docker.com/)

---

## 📋 Tổng Quan

UTH-ConfMS là hệ thống quản lý hội nghị khoa học toàn diện, được xây dựng theo kiến trúc microservices với các tính năng:

- ✅ **Quản lý Conference**: Tạo và cấu hình hội nghị, tracks, topics, deadlines
- ✅ **Call for Papers (CFP)**: Trang CFP công khai với yêu cầu nộp bài
- ✅ **Nộp Bài (Submission)**: Upload paper, quản lý authors, version control
- ✅ **Program Committee**: Quản lý PC members, COI detection
- ✅ **Review System**: Manual/auto assignment, scoring, discussions
- ✅ **Decision Making**: Accept/Reject decisions, bulk notifications
- ✅ **Camera-ready**: Thu thập phiên bản cuối, export proceedings
- ✅ **RBAC**: Role-based access control với conference scope
- ✅ **Notifications**: Email templates, in-app notifications
- ✅ **Audit Logs**: Comprehensive audit trail
- 🔄 **AI Tools** (Optional): Spell checking, summaries, reviewer matching

---

## 🏗️ Kiến Trúc Microservices

```
┌─────────────────────────────────────────────────────────┐
│            Frontend (ReactJS) - Port 3000              │
└──────────────────────┬─────────────────────────────────┘
                       │
┌──────────────────────▼─────────────────────────────────┐
│         API Gateway (Ocelot) - Port 5000              │
└─┬──────┬──────┬──────┬──────┬──────────────────────┬──┘
  │      │      │      │      │                      │
┌─▼──┐┌──▼──┐┌──▼──┐┌──▼──┐┌──▼──┐              ┌────▼────┐
│Iden││Conf ││Subm ││Rev  ││Noti│              │PostgreSQL│
│:5001││:5002││:5003││:5004││:5005│◄────────────┤  Redis  │
└────┘└─────┘└─────┘└─────┘└─────┘              └─────────┘
```

### Microservices

| Service | Port | Mô Tả |
|---------|------|-------|
| **Identity** | 5001 | User authentication, RBAC, SSO |
| **Conference** | 5002 | Conference management, CFP, tracks |
| **Submission** | 5003 | Paper submissions, file management |
| **Review** | 5004 | Review assignments, decisions |
| **Notification** | 5005 | Email & in-app notifications |
| **API Gateway** | 5000 | Routing, authentication |
| **Frontend** | 3000 | React web application |

---

## 🚀 Quick Start

### Yêu Cầu Hệ Thống

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL 15+](https://www.postgresql.org/) (nếu không dùng Docker)
- [Redis 7+](https://redis.io/) (nếu không dùng Docker)

### Deploy Toàn Bộ Hệ Thống với Docker Compose

```bash
# Build và start tất cả services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down
```

---

### Default Admin Account

```
Email: admin@uth.edu.vn
Password: Admin@123456
```

---

##   Cấu trúc thư mục dự án

```
UTH-Scientific-Conference-Paper-Management-System/
├── 📄 docker-compose.yml                # Cấu hình Docker cho production
│
├── 📂 database/                         # Database init scripts
│   ├── 01_identity_schema.sql           # Schema cho Identity Service
│   ├── 02_conference_schema.sql         # Schema cho Conference Service
│   ├── 03_submission_schema.sql         # Schema cho Submission Service
│   ├── 04_review_schema.sql             # Schema cho Review Service
│   ├── 05_notification_schema.sql       # Schema cho Notification Service
│   └── 06_admin_schema.sql              # Schema cho Admin (system configs, logs)
│
├── 📂 UTH-ConfMS-Docs/                  # Tài liệu dự án
│
├── 📂 UTH-ConfMS-Backend/               # Source code backend
│   ├── 📂 ApiGateway/                   # API Gateway (Port 5000)
│   │
│   ├── 📂 Services/
│   │   ├── 📂 Identity.Service/         # Port 5001
│   │   │   ├── Controllers/            # API endpoints
│   │   │   ├── Data/                   # DbContext, Migrations
│   │   │   ├── DTOs/                   # DTOs
│   │   │   ├── Entities/               # Entities
│   │   │   ├── Interfaces/             # Interfaces
│   │   │   ├── Mappings/               # AutoMapper profiles
│   │   │   ├── Repositories/           # Repository pattern
│   │   │   ├── Services/               # Business logic
│   │   │   └── Validators/             # FluentValidation
│   │   ├── 📂 Conference.Service/      # Port 5002 (cấu trúc tương tự)
│   │   ├── 📂 Submission.Service/      # Port 5003 (cấu trúc tương tự)
│   │   ├── 📂 Review.Service/          # Port 5004 (cấu trúc tương tự)
│   │   └── 📂 Notification.Service/    # Port 5005 (cấu trúc tương tự)
│   │
│   └── 📂 Shared/UTH.ConfMS.Shared/    # Shared library
│       ├── UTH.ConfMS.Shared.csproj
│       ├── Constants/                  # Hằng số dùng chung
│       └── Models/                     # Models dùng chung giữa các services
│
├── 📂 UTH-ConfMS-Frontend/             # React Frontend (Port 3000)
│   ├── Dockerfile
│   ├── index.html
│   ├── index.tsx
│   ├── metadata.json
│   ├── package.json
│   ├── vite.config.ts
│   ├── public/                         # Static files
│   └── src/
│       ├── App.tsx
│       ├── vite-env.d.ts
│       ├── api/
│       ├── assets/
│       ├── components/                  # React components
│       ├── contexts/
│       ├── pages/                       # React pages
│       └── services/                    # API service clients
```

---

## 🔧 Nhiệm vụ của các Service

### 1. **Identity.Service** (Port 5001)
| Nhiệm vụ | Mô tả |
|----------|-------|
| **Authentication** | Đăng nhập, đăng ký, JWT token |
| **Authorization** | Phân quyền Role-based (Admin, Author, Reviewer, Chair) |
| **User Management** | CRUD users, profiles |
| **Password Management** | Reset password, change password |
| **Audit Logging** | Ghi log hành động người dùng |

### 2. **Conference.Service** (Port 5002)
| Nhiệm vụ | Mô tả |
|----------|-------|
| **Conference CRUD** | Tạo, sửa, xóa hội nghị khoa học |
| **Timeline Management** | Quản lý các mốc thời gian (submission, review, notification) |
| **Track Management** | Quản lý các track/topic của hội nghị |
| **Committee Management** | Quản lý ban tổ chức, Program Committee |
| **Registration** | Đăng ký tham gia hội nghị |

### 3. **Submission.Service** (Port 5003)
| Nhiệm vụ | Mô tả |
|----------|-------|
| **Paper Submission** | Nộp bài báo khoa học |
| **File Management** | Upload/download files (PDF, LaTeX) |
| **Version Control** | Quản lý các phiên bản của bài nộp |
| **Author Management** | Quản lý danh sách tác giả |
| **Status Tracking** | Theo dõi trạng thái: Draft → Submitted → Under Review → Accepted/Rejected |

### 4. **Review.Service** (Port 5004)
| Nhiệm vụ | Mô tả |
|----------|-------|
| **Reviewer Assignment** | Phân công reviewer cho bài báo |
| **Review Management** | Quản lý quá trình review |
| **Scoring** | Chấm điểm theo tiêu chí |
| **Conflict of Interest** | Phát hiện xung đột lợi ích |
| **Decision Making** | Quyết định Accept/Reject/Revision |
| **Rebuttal** | Quản lý phản hồi của tác giả |

### 5. **Notification.Service** (Port 5005)
| Nhiệm vụ | Mô tả |
|----------|-------|
| **Email Notifications** | Gửi email thông báo |
| **In-app Notifications** | Thông báo trong ứng dụng |
| **Templates** | Quản lý email templates |
| **Scheduling** | Lên lịch gửi thông báo |
| **Preferences** | Cài đặt tùy chọn nhận thông báo |

### 6. **API Gateway** (Port 5000)
| Nhiệm vụ | Mô tả |
|----------|-------|
| **Routing** | Điều hướng request đến đúng service |
| **Load Balancing** | Cân bằng tải |
| **Rate Limiting** | Giới hạn số request |
| **Authentication** | Xác thực JWT token tập trung |
| **CORS** | Xử lý Cross-Origin requests |

### 7. **Frontend** (Port 3000)
| Nhiệm vụ | Mô tả |
|----------|-------|
| **User Interface** | Giao diện người dùng React + Material-UI |
| **API Integration** | Gọi API qua Gateway |
| **State Management** | Quản lý trạng thái ứng dụng |
| **Routing** | Điều hướng SPA |

---

## 🔄 Luồng hoạt động của hệ thống

### Tổng quan kiến trúc

```
┌─────────────────────────────────────────────────────────────────────────┐
│                         FRONTEND (:3000)                                │
│                       React + Material-UI                               │
└────────────────────────────────┬────────────────────────────────────────┘
                                 │
                                 ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                        API GATEWAY (:5000)                              │
│                         Ocelot Routing                                  │
└────┬────────┬────────┬────────┬────────┬────────────────────────────────┘
     │        │        │        │        │
     ▼        ▼        ▼        ▼        ▼
┌────────┐┌────────┐┌────────┐┌────────┐┌────────┐
│Identity││Conferen││Submissi││ Review ││Notifica│
│ :5001  ││ :5002  ││ :5003  ││ :5004  ││ :5005  │
└───┬────┘└───┬────┘└───┬────┘└───┬────┘└───┬────┘
    │         │         │         │         │
    └─────────┴─────────┴────┬────┴─────────┘
                             │
              ┌──────────────┼──────────────┐
              ▼              ▼              ▼
       ┌──────────┐   ┌──────────┐   ┌──────────┐
       │PostgreSQL│   │  Redis   │   │  Shared  │
       │  :5432   │   │  :6379   │   │  Library │
       └──────────┘   └──────────┘   └──────────┘
```

### Luồng 1: Đăng ký & Đăng nhập (Authentication)

```
User ──▶ Frontend ──▶ API Gateway ──▶ Identity Service ──▶ PostgreSQL
                                            │
                                            ▼
                                         Redis (session)
```

**Các bước:**
1. User nhập email/password trên Frontend
2. Frontend gọi `POST /api/auth/login` qua API Gateway
3. Gateway route đến Identity.Service
4. Identity.Service kiểm tra credentials, tạo JWT Token
5. Frontend lưu token, các request tiếp theo gửi kèm `Authorization: Bearer <token>`

### Luồng 2: Nộp bài báo (Paper Submission)

```
Author ──▶ Frontend ──▶ Gateway ──▶ Submission Service ──▶ PostgreSQL
              │                           │
              │                           ▼
              │                      File Storage
              │                           │
              └───────────────────────────┼──────▶ Notification Service
                                          │              │
                                          │              ▼
                                          │        Email (confirm)
```

**Các bước:**
1. Author upload PDF và điền thông tin bài báo
2. Submission.Service validate, lưu file, tạo record
3. Notification.Service gửi email xác nhận

### Luồng 3: Quá trình Review

```
Chair ──▶ Frontend ──▶ Gateway ──▶ Review Service ──▶ PostgreSQL
                                        │
                                        ├──▶ Submission Service (get paper)
                                        │
                                        └──▶ Notification Service (notify reviewer)
                                                      │
                                                      ▼
Reviewer ◀─────────────────────────────────── Email Invitation
    │
    └──▶ Frontend ──▶ Gateway ──▶ Review Service (submit review)
```

**Các bước:**
1. Chair assign reviewer cho submission
2. Review.Service kiểm tra Conflict of Interest
3. Notification.Service gửi email mời reviewer
4. Reviewer đọc paper, submit review với điểm số
5. Chair xem reviews, đưa ra quyết định Accept/Reject

### Luồng 4: State Machine - Submission Status

```
┌─────────┐
│  DRAFT  │
└────┬────┘
     │ submit
     ▼
┌──────────┐
│SUBMITTED │
└────┬─────┘
     │ assign reviewers
     ▼
┌────────────────┐
│ UNDER_REVIEW   │
└───────┬────────┘
        │ all reviews completed
        ▼
┌───────┴───────┬──────────────┐
│               │              │
▼               ▼              ▼
ACCEPTED    REJECTED    REVISION_REQUIRED
                              │
                              │ submit revision
                              ▼
                        UNDER_REVIEW (again)
```

### Luồng 5: Authentication Flow (JWT)

```
Frontend                Gateway                    Backend Service
   │                       │                             │
   │  Request + JWT Token  │                             │
   │──────────────────────▶│                             │
   │                       │  Validate JWT               │
   │                       │  (signature, expiry)        │
   │                       │────────────────────────────▶│
   │                       │         Response            │
   │      Response         │◀────────────────────────────│
   │◀──────────────────────│                             │
```

---

## 🎭 Roles & Permissions

| Role | Permissions |
|------|-------------|
| **Admin** | Quản lý toàn hệ thống, users, system configs |
| **Chair** | Tạo/quản lý conference, assign reviewers, quyết định accept/reject |
| **Reviewer** | Xem submissions được assign, submit reviews |
| **Author** | Submit papers, xem reviews, submit rebuttals |
| **Attendee** | Đăng ký tham dự, xem accepted papers |

---

##  📊 Database Schema

Hệ thống sử dụng PostgreSQL với 6 database schemas:

### 1. Identity Service (01_identity_schema.sql)
- `users`, `roles`, `permissions`
- `user_roles`, `role_permissions`
- `refresh_tokens`, `sso_providers`
- `audit_logs`

### 2. Conference Service (02_conference_schema.sql)
- `conferences`, `conference_tracks`
- `conference_topics`, `conference_deadlines`
- `call_for_papers`, `email_templates`
- `conference_committee`

### 3. Submission Service (03_submission_schema.sql)
- `submissions`, `authors`
- `submission_files`, `supplementary_materials`
- `conflicts_of_interest`, `plagiarism_checks`

### 4. Review Service (04_review_schema.sql)
- `review_assignments`, `reviews`
- `review_discussions`, `rebuttals`
- `decisions`, `reviewer_expertise`

### 5. Notification Service (05_notification_schema.sql)
- `notification_queue`, `email_logs`
- `smtp_settings`, `notification_campaigns`
- `in_app_notifications`

### 6. Admin Service (06_admin_schema.sql)
- `tenants`, `system_settings`
- `audit_logs`, `backup_history`
- `scheduled_jobs`, `feature_flags`

---

## 📚 Tài Liệu Chi Tiết

| Document | Mô Tả |
|----------|-------|
| [ARCHITECTURE.md](docs/ARCHITECTURE.md) | Kiến trúc hệ thống, design patterns |
| [DATABASE.md](docs/DATABASE.md) | Database schema, ERD, queries |
| [API.md](docs/API.md) | API documentation, endpoints |
| [INSTALLATION.md](docs/INSTALLATION.md) | Hướng dẫn cài đặt chi tiết |
| [PROJECT_STRUCTURE.md](docs/PROJECT_STRUCTURE.md) | Cấu trúc project, file organization |

---

## 🔐 Security Features

- ✅ JWT Authentication với refresh tokens
- ✅ Role-Based Access Control (RBAC)
- ✅ Conference-scoped permissions
- ✅ Double-blind / Single-blind review modes
- ✅ Conflict of Interest (COI) detection
- ✅ SSO support (Google, Microsoft, ORCID)
- ✅ Password hashing với BCrypt
- ✅ Comprehensive audit logs
- ✅ HTTPS ready
- ✅ Rate limiting

---

## 🎯 Main Features

### For Authors
- Register và tạo profile
- Submit papers với metadata
- Upload supplementary materials
- Track submission status
- View anonymized reviews
- Upload camera-ready versions
- Receive email notifications

### For Reviewers
- Accept/Decline review invitations
- Bid on papers (optional)
- Submit detailed reviews
- Participate in PC discussions
- Track review deadlines
- View reviewer load

### For Chairs
- Create và configure conferences
- Manage tracks và topics
- Set deadlines
- Invite PC members
- Assign reviewers (manual/auto)
- Monitor review progress
- Make decisions
- Send bulk notifications
- Export proceedings

### For Admins
- Multi-tenancy management
- System settings
- View audit logs
- Backup/Restore
- Monitor system health
- Manage scheduled jobs
- Feature flags control

---


## 🙏 Acknowledgments

Hệ thống được phát triển với mục đích:
- Quản lý hội nghị khoa học chuyên nghiệp
- Hỗ trợ quy trình peer review
- Tăng tính minh bạch và công bằng
- Tiết kiệm chi phí so với giải pháp thương mại (EasyChair, ConfTool)

---

**Made with ❤️ for Academic Research Community**