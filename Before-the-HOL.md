Serverless Computing using-Azure Functions
FY24

### 参考情報

- [名前付け規則を定義する](https://learn.microsoft.com/ja-jp/azure/cloud-adoption-framework/ready/azure-best-practices/resource-naming)

- [Azure リソースの種類に推奨される省略形](https://learn.microsoft.com/ja-jp/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations)

<br />

### 共通リソースの展開

<br />

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fhiroyay-ms%2FServerless-Computing-using-Azure-Functions%2Fmain%2Ftemplates%2Fdeploy-vnet-hub.json)

### パラメーター

- **virtualNetwork**: 仮想ネットワーク名（2 ～ 64 文字/英数字、アンダースコア、ピリオド、およびハイフン）

- **addressPrefix**: IPv4 アドレス空間

- **subnet1**: サブネットの名前 (1)（1 ～ 80 文字/英数字、アンダースコア、ピリオド、およびハイフン）

- **subnet1Prefix**: サブネット アドレス範囲 (1)

- **bastionPrefix**: AzureBastionSubnet サブネットのアドレス範囲

- **bastionHost**: Bastion リソースの名前（1 ～ 80 文字/英数字、アンダースコア、ピリオド、およびハイフン）

※ 事前にリソース グループの作成が必要

※ 選択したリソース グループのリージョンにすべてのリソースを展開

<br />

### リソースの展開

<br />

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fhiroyay-ms%2FServerless-Computing-using-Azure-Functions%2Fmain%2Ftemplates%2Fdeploy-resources.json)

### パラメーター

- **virtualNetwork**: 仮想ネットワーク名（2 ～ 64 文字/英数字、アンダースコア、ピリオド、およびハイフン）

- **addressPrefix**: IPv4 アドレス空間

- **subnet1**: サブネットの名前 (1)（1 ～ 80 文字/英数字、アンダースコア、ピリオド、およびハイフン）

- **subnet1Prefix**: サブネット アドレス範囲 (1)

- **subnet2**: サブネットの名前 (1)（1 ～ 80 文字/英数字、アンダースコア、ピリオド、およびハイフン）

- **subnet2Prefix**: サブネット アドレス範囲 (1)

- **subnet3**: サブネットの名前 (1)（1 ～ 80 文字/英数字、アンダースコア、ピリオド、およびハイフン）

- **subnet3Prefix**: サブネット アドレス範囲 (1)

- **subnetvm**: サブネットの名前 (1)（1 ～ 80 文字/英数字、アンダースコア、ピリオド、およびハイフン）

- **subnetvmPrefix**: サブネット アドレス範囲 (1)

- **apiManagement**: API Management (50 文字以下/英数字、およびハイフン)

- **publisherEmail**: 通知を受信するためのメール アドレス

- **logAnalyticsWorkspace**: Log Analytics ワークスペース (4 ～ 63 文字/英数字、およびハイフン)

- **keyVaultName**: Key Vault (3 ～ 24 文字/英数字、およびハイフン)

- **sqlServer**: SQL Server (1 ～ 63 文字/英子文字、数字、およびハイフン)

- **sqlAdministratorLogin**: SQL Server 管理者 (英数字)

- **sqlAdministratorPassword**: 管理者パスワード (8 ～ 128 文字/英大文字、英小文字、数字 (0 ～ 9)、英数字以外の文字のうち３つを含む)

※ 事前にリソース グループの作成が必要

※ 選択したリソース グループのリージョンにすべてのリソースを展開

※ 共通リソースで Bastion を展開した後は、リソース展開後に手動で VNet Peering を構成

※ Key Vault のキー コンテナー管理者ロールにワークショップで使用するユーザーを追加
