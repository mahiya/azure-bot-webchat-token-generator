# このリポジトリについて
このリポジトリを使用することで、Azure Bot Service の Web チャットを Web ページに埋め込む際に使用する、トークンを発行する Web API を Azure Functions 上にデプロイすることができます。

## 必要な環境
- [Azure CLI](https://docs.microsoft.com/ja-jp/cli/azure/install-azure-cli)
- [Azure Bicep](https://docs.microsoft.com/ja-jp/azure/azure-resource-manager/bicep/install)
- [.NET 6.0](https://dotnet.microsoft.com/ja-jp/download/dotnet/6.0)
- [Azure Functions Core Tools](https://learn.microsoft.com/ja-jp/azure/azure-functions/functions-run-local)
- bash が実行できるコンソール (Windows だと [Git Bash](https://gitforwindows.org/) など)

## デプロイ方法

Azure CLI で Azure テナントへログインし、使用するサブスクリプションを選択します。
```bash
az login
az account set --subscription "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
```

> 参考：[Azure CLI を使用して Azure サブスクリプションを管理する方法](https://learn.microsoft.com/ja-jp/cli/azure/manage-azure-subscriptions-azure-cli)

環境を構築するために、```deploy.sh```スクリプトを実行します。
```bash
./deploy.sh
```

スクリプトを実行後、スクリプト内でデプロイ先のリソースグループ名と、対象となる Azure Bot Service の Web チャットの秘密キーを指定してください。
```bash
$ ./deploy.sh
Please input Resource Group name to deploy:
rg-azbot-token-gen
Please input Web Chat Key of Azure Bot Service to generate token by Azure Function:
xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
```

> 参考：[ボットの秘密鍵を取得する](https://learn.microsoft.com/ja-jp/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-authentication?view=azure-bot-service-4.0)
> 参考：[ボットを Web チャットに接続する](https://learn.microsoft.com/ja-jp/azure/bot-service/bot-service-channel-connect-webchat?view=azure-bot-service-4.0#production-embedding-option)