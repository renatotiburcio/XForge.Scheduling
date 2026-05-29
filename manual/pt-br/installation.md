> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 05 — Instalação

Guia completo de instalação do XForge.Scheduling em projetos .NET.

## Pré-requisitos

| Requisito | Versão mínima |
|-----------|--------------|
| .NET SDK | 8.0+ |
| C# | 12+ |
| IDE | VS 2022 17.8+, Rider 2023.3+, VS Code |

## Instalação via .NET CLI

`ash
dotnet add package XForge.Scheduling
`

## PackageReference em .csproj

`xml
<ItemGroup>
  <PackageReference Include="XForge.Scheduling" Version="0.4.*" />
</ItemGroup>
`

## Central Package Management

`xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="XForge.Scheduling" Version="0.4.*" />
  </ItemGroup>
</Project>
`

## Instalação a partir do código-fonte

`ash
git clone https://github.com/renatotiburcio/XForge.Scheduling.git
cd XForge.Scheduling
dotnet restore
dotnet build --configuration Release
`

---

**Navegação:** ← [README](./README.md) | → [Quick Start](./quick-start.md)
