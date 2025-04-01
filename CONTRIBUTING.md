# 貢獻指南

感謝您對票務系統專案的興趣！本文件將指導您如何參與專案開發。

## 開發環境設置

1. 克隆專案
```bash
git clone [repository-url]
cd TicketSystem
```

2. 安裝必要工具
- .NET 8 SDK
- Visual Studio 2022 或 Visual Studio Code
- Git

3. 還原套件
```bash
dotnet restore
```

4. 設定開發環境
- 複製 `appsettings.Development.json.example` 到 `appsettings.Development.json`
- 修改連接字串和設定

## 開發流程

### 1. 分支管理
- `main`: 主分支，用於生產環境
- `develop`: 開發分支，用於整合功能
- `feature/*`: 功能分支，用於開發新功能
- `bugfix/*`: 修復分支，用於修復問題
- `release/*`: 發布分支，用於準備新版本

### 2. 開發新功能
1. 從 `develop` 分支建立新的功能分支
```bash
git checkout develop
git pull
git checkout -b feature/your-feature-name
```

2. 開發功能
3. 提交變更
```bash
git add .
git commit -m "feat: 描述你的變更"
```

4. 推送到遠端
```bash
git push origin feature/your-feature-name
```

5. 建立 Pull Request 到 `develop` 分支

### 3. 程式碼規範

#### 命名規範
- 類別名稱：PascalCase
- 方法名稱：PascalCase
- 變數名稱：camelCase
- 常數名稱：UPPER_CASE
- 介面名稱：IPascalCase

#### 程式碼風格
- 使用 4 個空格進行縮排
- 使用 UTF-8 編碼
- 檔案結尾使用 LF
- 移除未使用的 using 語句
- 適當使用空行分隔邏輯區塊

#### 註解規範
- 類別和公共方法必須有 XML 文件註解
- 複雜邏輯需要適當的程式碼註解
- 使用中文註解說明業務邏輯

### 4. 提交訊息規範
使用 Conventional Commits 規範：
```
<type>(<scope>): <description>

[optional body]

[optional footer]
```

類型：
- `feat`: 新功能
- `fix`: 修復問題
- `docs`: 文件變更
- `style`: 程式碼格式變更
- `refactor`: 重構
- `test`: 測試相關
- `chore`: 建置過程或輔助工具的變動

### 5. 測試要求
- 新增功能必須包含單元測試
- 修改現有功能必須更新相關測試
- 確保所有測試通過
- 測試覆蓋率不低於 80%

## 審查流程

1. 程式碼審查
   - 確保符合程式碼規範
   - 檢查測試覆蓋率
   - 確認功能完整性

2. 合併條件
   - 所有測試通過
   - 審查者核准
   - 解決所有衝突

## 發布流程

1. 版本號規範
   - 主版本號：重大變更
   - 次版本號：新功能
   - 修訂號：問題修復

2. 發布步驟
   - 從 `develop` 建立 `release` 分支
   - 更新版本號
   - 更新更新日誌
   - 合併到 `main` 和 `develop`

## 問題回報

1. 使用 GitHub Issues 回報問題
2. 提供詳細的問題描述
3. 附上重現步驟
4. 提供相關的錯誤訊息或日誌

## 聯絡方式

如有任何問題，請透過以下方式聯絡：
- 建立 Issue
- 發送郵件至 [email]

感謝您的貢獻！ 