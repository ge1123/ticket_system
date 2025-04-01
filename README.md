# 票務系統 (Ticket System)

這是一個基於 Clean Architecture 設計的票務系統，使用 .NET 8 開發。

## 專案架構

專案採用 Clean Architecture 架構，分為以下層級：

- **Domain Layer**: 核心領域層，包含業務邏輯和規則
- **Application Layer**: 應用層，處理用例和業務流程
- **Infrastructure Layer**: 基礎設施層，實現外部服務整合
- **API Layer**: 表現層，處理 HTTP 請求和回應

## 技術棧

- **.NET 8**
- **Clean Architecture**
- **CQRS Pattern** (使用 MediatR)
- **Entity Framework Core**
- **AutoMapper**
- **FluentValidation**
- **RabbitMQ**
- **Redis**
- **Serilog**

## 開發環境需求

- .NET 8 SDK
- SQL Server
- Redis
- RabbitMQ

## 專案結構

```
TicketSystem/
├── Domain/                 # 核心領域層
│   ├── Entities/          # 領域實體
│   ├── ValueObjects/      # 值物件
│   ├── Enums/            # 列舉
│   ├── Interfaces/       # 領域介面
│   └── Exceptions/       # 領域異常
├── Application/           # 應用層
│   ├── Common/           # 通用功能
│   ├── Features/         # 功能模組
│   ├── Interfaces/       # 應用介面
│   ├── Models/          # 資料模型
│   └── Mappings/        # 物件映射
├── Infrastructure/        # 基礎設施層
│   ├── Persistence/      # 資料持久化
│   ├── Services/        # 外部服務
│   ├── Messaging/       # 消息佇列
│   ├── Caching/        # 快取服務
│   └── Logging/        # 日誌服務
└── Api/                  # 表現層
    ├── Controllers/     # API 控制器
    ├── Middlewares/    # 中間件
    ├── Filters/        # 過濾器
    └── Models/         # API 模型
```

## 開始使用

1. 克隆專案
```bash
git clone [repository-url]
```

2. 還原套件
```bash
dotnet restore
```

3. 設定資料庫連接字串
   - 在 `appsettings.json` 中設定資料庫連接字串
   - 在 `appsettings.Development.json` 中設定開發環境特定配置

4. 執行資料庫遷移
```bash
dotnet ef database update
```

5. 運行專案
```bash
dotnet run --project TicketSystem.Api
```

## API 文檔

啟動專案後，可以通過以下 URL 訪問 Swagger 文檔：
- https://localhost:7001/swagger

## 開發指南

### 新增功能

1. 在 Domain 層定義領域模型和介面
2. 在 Application 層實現業務邏輯
3. 在 Infrastructure 層實現外部服務整合
4. 在 API 層新增控制器和端點

### 程式碼規範

- 遵循 Clean Architecture 原則
- 使用 CQRS 模式處理命令和查詢
- 實作 Repository 模式進行資料存取
- 使用 FluentValidation 進行資料驗證
- 使用 AutoMapper 進行物件映射

## 授權

[授權類型]

## 貢獻指南

[貢獻指南內容]
