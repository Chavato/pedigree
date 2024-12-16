Migration:
	dotnet ef --startup-project .\backend\Pedigree.WebApi\Pedigree.WebApi.csproj migrations add "initial migration" --project .\backend\Pedigree.Infra\Pedigree.Infra.Data\Pedigree.Infra.Data.csproj
