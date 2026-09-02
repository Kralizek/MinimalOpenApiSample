# MinimalOpenApiSample

A small bookstore API and web app that demonstrates a contract-first workflow for ASP.NET Core Minimal APIs using [MinimalOpenAPI](https://github.com/Kralizek/MinimalOpenApi).

Instead of generating OpenAPI from C# endpoints, this sample treats the OpenAPI document as the source of truth and generates endpoint scaffolding at build time.

## Why this sample exists

In many projects, code-first OpenAPI generation is the easiest and best choice.

This sample focuses on the cases where contract-first is valuable:
- You need to review API changes as contract changes first.
- You collaborate across teams or organizations using OpenAPI as the integration artifact.
- You want schema-first iteration before implementation details are finalized.

The intent is to keep hand-written code focused on business behavior while repetitive endpoint plumbing is generated.

## Current package/version context

This sample currently uses:
- `MinimalOpenAPI` version `1.0.0`
- `.NET SDK` pinned via `global.json` to `10.0.201` (roll-forward: latest minor)

## Repository layout

- `bookstore.yml`: the authored OpenAPI contract (source of truth)
- `src/BookStore.Api`: Minimal API project using generated endpoint base classes
- `src/BookStore.Web`: simple Blazor UI consuming the API
- `src/BookStore.AppHost`: Aspire AppHost to run API + web together

## How it works

1. The OpenAPI spec is authored in `bookstore.yml`.
2. The API project references it with an `<OpenApi ... />` item in the project file.
3. `MinimalOpenAPI` generates contract types and endpoint scaffolding at build time.
4. You implement endpoint behavior by inheriting generated endpoint base classes.
5. The API maps generated endpoints with:
   - `builder.Services.AddMinimalOpenApi();`
   - `app.MapMinimalOpenApiEndpoints();`

The sample also exposes the authored schema file through `MapOpenApiSchemas()` and wires it into Swagger UI.

## Prerequisites

- .NET 10 SDK (or compatible with the pinned `global.json`)

## Run the sample

From the repository root:

```bash
dotnet run --project src/BookStore.AppHost
```

This starts both API and web projects through Aspire.

## Useful endpoints

When running the API directly with the default launch profile:
- Swagger UI: `https://localhost:7185/swagger/index.html`
- Published OpenAPI schema: `https://localhost:7185/schemas/openapi.yaml`

Main API operations from the contract:
- `GET /books` (search + pagination)
- `POST /books`
- `GET /books/{id}`
- `PUT /books/{id}`
- `DELETE /books/{id}`
- `GET /authors`
- `GET /categories`

## Notes about persistence

The API uses SQLite and creates a local database file under:
- `.data/bookstore.db`

The database is created automatically at startup if it does not exist.

## Related reading and context

- Blog post: [Making OpenAPI the source of truth for ASP.NET Core Minimal APIs](https://renatogolia.com/2026/04/13/openapi-source-of-truth-minimal-apis/)
- Library repository: [Kralizek/MinimalOpenApi](https://github.com/Kralizek/MinimalOpenApi)
- NuGet package: [MinimalOpenAPI](https://www.nuget.org/packages/MinimalOpenAPI)
- Reddit discussion: [Generating ASP.NET Core Minimal API Endpoints from OpenAPI: Contract-First with Source Generators](https://www.reddit.com/r/dotnet/comments/1snznpg/generating_aspnet_core_minimal_api_endpoints_from/)
- LinkedIn post: <https://www.linkedin.com/feed/update/urn:li:activity:7449583289562112000/>

Notes:

- LinkedIn content may require sign-in to view.
- The original blog post was written before this sample was updated to 1.0.0, so some APIs and wording in the post may be slightly behind the current sample.

## Is this always the right approach?

No. This is not meant to replace code-first in all scenarios.

If your OpenAPI document is mostly generated documentation, code-first is often simpler.

If your contract must be authored, reviewed, and versioned intentionally before or alongside implementation, this contract-first model can be a strong fit.
