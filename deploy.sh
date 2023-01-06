# Define the region and resource group name to deploy
region=japaneast
httpTriggerFunctionName="GenerateBotToken"

# Get Resource Group name
echo "Please input Resource Group name to deploy:"
read input
resourceGroupName=$input

# Get Web Chat Key of Azure Bot Service
echo "Please input Web Chat Key of Azure Bot Service to generate token by Azure Function:"
read input
botServiceApiKey=$input

# Create a resource group
az group create \
    --location $region \
    --resource-group $resourceGroupName

# Deploy the Bicep template
functionAppName=`az deployment group create \
    --resource-group $resourceGroupName \
    --template-file deploy.bicep \
    --parameters botServiceApiKey=$botServiceApiKey \
    --query "properties.outputs.functionAppName.value" \
    --output tsv`

# Deploy functions
func azure functionapp publish $functionAppName --csharp
