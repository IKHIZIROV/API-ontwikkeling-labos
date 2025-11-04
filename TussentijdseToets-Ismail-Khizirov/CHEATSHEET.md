# 🧠 Cheatsheet — Tussentijdse Toets API-Ontwikkeling (Tot en met Hoofdstuk 4: Logging)

## 📚 Inhoudsopgave / Navigatie

1. [🧩 Inleiding](#-inleiding)
2. [⚙️ Oefening 1: Logging met Seq](#-oefening-1-loggen-met-seq)
3. [🌍 Oefening 2: Omgevingsspecifieke Meldingen (Environments)](#-oefening-2--omgevingsspecifieke-meldingen-environments)
4. [🧭 Oefening 3: Routing (met Services, Interface en Controller)](#-oefening-3--routing-met-services-interface-en-controller)

---

# 🧩 Inleiding

Deze **cheatsheet** is een samenvatting van alle oefeningen voor de **tussentijdse toets API-Ontwikkeling**.  
Ze bevat alles wat je moet weten tot **hoofdstuk 4: Logging**.
Deze cheatsheat is volledig gemaakt door Ismail Khizirov (en met een beetje hulp van ChatGPT).

Elke oefening bevat:  
- Een duidelijke uitleg van het doel 🎯  
- Stap-voor-stap instructies 🧱  
- Voorbeelden van code 💻  
- Testscenario’s en valkuilen ⚠️  
- Samenvattende tabellen 🧾  

Gebruik dit document tijdens het examen als **referentie en geheugensteun**.  
> 💡 Let op: gebruik het als hulp, niet als kant-en-klare oplossing — pas de voorbeelden aan aan je eigen projectnamen.

---

## 🎯 Oefening 1: Loggen met Seq

Doel van deze oefening:  
Een **ASP.NET Core Web API** configureren met **Serilog** zodat logberichten zichtbaar zijn in **Seq**.

---

## ⚙️ Stap 1: Installeer de benodigde NuGet-packages

Open de **Package Manager Console** en installeer deze packages:

```bash
Serilog.AspNetCore
Serilog.Sinks.Console
Serilog.Sinks.Seq
```

> 💡 **Uitleg:**
> - `Serilog.AspNetCore` → integreert Serilog in de ASP.NET Core pipeline  
> - `Serilog.Sinks.Console` → toont logs in de terminal  
> - `Serilog.Sinks.Seq` → stuurt logs naar de Seq-server (`http://localhost:5341`)

---

## 🧩 Stap 2: Configureer `Program.cs`

Kopieer en plak onderstaande code **bovenaan** in `Program.cs` (vóór `builder.Build()`):

```csharp
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Verwijder standaard logproviders
builder.Logging.ClearProviders();

// 2️⃣ Configureer Serilog
var logger = new LoggerConfiguration()
    // Log naar tekstbestand in JSON-formaat
    .WriteTo.File(
        formatter: new JsonFormatter(),
        path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log.txt"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 90)
    // Log naar console
    .WriteTo.Console(new JsonFormatter())
    // Log naar Seq (start Seq lokaal!)
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

// 3️⃣ Voeg Serilog toe aan de logging pipeline
builder.Logging.AddSerilog(logger);

var app = builder.Build();

app.MapControllers();
app.Run();
```

---

## 🧱 Stap 3: Maak `LoggingController.cs`

Deze controller zal logberichten uitsturen naar Seq wanneer endpoints worden aangeroepen.

```csharp
using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        // 🟢 Voorbeeld: Booking log
        [HttpGet("bookings/book")]
        public IActionResult LogBooking()
        {
            _logger.LogInformation("Boeking uitgevoerd op {Time}", DateTime.Now);
            return Ok("Booking log created");
        }

        // 🟡 Voorbeeld: Cancel log
        [HttpGet("bookings/cancel")]
        public IActionResult LogCancel()
        {
            _logger.LogWarning("Boeking geannuleerd op {Time}", DateTime.Now);
            return Ok("Cancel log created");
        }

        // 🔴 Extra voorbeeld: Error log
        [HttpGet("bookings/error")]
        public IActionResult LogError()
        {
            _logger.LogError("Fout bij verwerken van boeking op {Time}", DateTime.Now);
            return Ok("Error log created");
        }
    }
}
```

---

### 🧠 Tip: Kies het juiste logniveau

Gebruik het **passende logniveau** voor elke situatie:

| Methode | Niveau | Gebruik wanneer... |
|----------|---------|--------------------|
| `LogTrace()` | 🔍 Zeer gedetailleerde debugging-informatie. |
| `LogDebug()` | 🧰 Debug-informatie tijdens ontwikkeling. |
| `LogInformation()` | 🟢 Normale operationele logs (zoals "booking created"). |
| `LogWarning()` | 🟡 Mogelijke problemen of uitzonderingen. |
| `LogError()` | 🔴 Fouten die hersteld moeten worden. |
| `LogCritical()` | 💀 Ernstige fouten, app kan crashen. |

---

## 🧪 Stap 4: Test je logs met Seq

### ✅ 1. Installeer en start Seq

- Download via: [https://datalust.co/seq](https://datalust.co/seq)
- Standaard draait Seq op:  
  **http://localhost:5341**

---

### ✅ 2. Run je Web API

Start het project in **Visual Studio**

---

### ✅ 3. Open Seq in je browser

Surf naar:

```
http://localhost:5341/
```

Je zou de logberichten moeten zien verschijnen in **real time**:
- 🟢 Booking log (`LogInformation`)
- 🟡 Cancel log (`LogWarning`)
- 🔴 Error log (`LogError`)

---

## 💡 Extra Tips

- In `appsettings.Development.json` kan je logginginstellingen omgevingsspecifiek maken.

---

## 🧾 Samenvatting

| Stap | Actie | Belangrijk |
|------|--------|-------------|
| 1 | Installeer NuGet packages | Serilog + Seq |
| 2 | Configureer Serilog in `Program.cs` | Gebruik `WriteTo.Seq()` |
| 3 | Maak controller met endpoints | Gebruik `_logger.LogInformation()` enz. |
| 4 | Test met Seq | Controleer of logs verschijnen |

---

✅ **Resultaat:**  
Je hebt nu een werkende logging setup met **Serilog → Seq → ASP.NET Web API**  
Perfect voor het examenonderdeel *Logging met Seq*! 🎓🔥

-------------------

# 🌍 Oefening 2 — Omgevingsspecifieke Meldingen (Environments)

> **Doel:** Toon een **exacte** boodschap op `/environment-message` afhankelijk van de **actieve omgeving** (Development, Staging, Production).  
> **Punten:** 3 (1 per omgeving). Zorg dat de strings exact overeenkomen met de opgave.

---

## ✅ Overzicht van wat je krijgt
- **Strakke structuur** met duidelijke stappen
- **Volledige en correcte code** voor zowel **Controller** als **Minimal API** (kies één)
- **Kant-en-klare `appsettings.*.json` voorbeelden**
- **`launchSettings.json` profielen** per omgeving
- **Testscenario’s**, **checklist** en **valkuilen**

---

## 1) Zet je environment-bestanden klaar

Maak naast `appsettings.json` ook de volgende bestanden (hoofdlettergevoelig!):

```
appsettings.Development.json
appsettings.Staging.json
appsettings.Production.json
```

> ℹ️ Voeg de messages toe aan de juiste bestanden, hangt af van wat de opdracht vraagt maar dit is een voorbeeld:

**`appsettings.Development.json`:**
```json
{
  "AppSettings": {
    "EnvironmentMessage": "Je draait de applicatie in de Development-omgeving."
  }
}
```

**`appsettings.Staging.json`:**
```json
{
  "AppSettings": {
    "EnvironmentMessage": "Je draait de applicatie in de Staging-omgeving."
  }
}
```

**`appsettings.Production.json`:**
```json
{
  "AppSettings": {
    "EnvironmentMessage": "Je draait de applicatie in de Production-omgeving."
  }
}
```


---

## 2) Koppel je profielen aan de juiste omgeving

Open `Properties/launchSettings.json` en zorg voor **3 profielen**, elk met een andere `ASPNETCORE_ENVIRONMENT`:
Dit is een voorbeeld, je moet meestal alleen de environmentVariables toevoegen aan bestaande

```json
{
  "profiles": {
    "Api (Development)": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "environment-message",
      "applicationUrl": "https://localhost:7146;http://localhost:5146",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Api (Staging)": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "environment-message",
      "applicationUrl": "https://localhost:7246;http://localhost:5246",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging"
      }
    },
    "Api (Production)": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "environment-message",
      "applicationUrl": "https://localhost:7346;http://localhost:5346",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Production"
      }
    }
  }
}
```

> ⚠️ **Hoofdlettergevoelig**: de waarde moet exact overeenkomen met de bestandsnaam (`Development`, `Staging`, `Production`).

---

## 3) Maak het endpoint (ofwel met Controller ofwel met Minimal API afhankelijk van wat er gevraagd word)

### 🅰️ Aanpak A — **Controller** (in een aparte bestand zoals EnvironmentsController.cs)

📄 **`Controllers/EnvironmentController.cs`**

```csharp
using Microsoft.AspNetCore.Mvc;

namespace VacationRentalApi.Controllers
{
    [ApiController]
    [Route("")]
    public class EnvironmentController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public EnvironmentController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET /environment-message
        [HttpGet("environment-message")]
        public IActionResult GetEnvironmentMessage()
        {
            // ✨ EXACTE teksten zoals gevraagd in de opdracht
            string message = _env.EnvironmentName switch
            {
                "Development" => "Je draait de applicatie in de Development-omgeving.",
                "Staging" => "Je draait de applicatie in de Staging-omgeving.",
                "Production" => "Je draait de applicatie in de Production-omgeving.",
                _ => $"Je draait de applicatie in de {_env.EnvironmentName}-omgeving."
            };

            return Ok(new { message });
        }
    }
}
```

---

### 🅱️ Aanpak B — **Minimal API** (in Program.cs)

📄 **`Program.cs` (fragment)**

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // als je ook controllers gebruikt

var app = builder.Build();

// Endpoint zonder controller:
app.MapGet("/environment-message", (IWebHostEnvironment env) =>
{
    var message = env.EnvironmentName switch
    {
        "Development" => "Je draait de applicatie in de Development-omgeving.",
        "Staging" => "Je draait de applicatie in de Staging-omgeving.",
        "Production" => "Je draait de applicatie in de Production-omgeving.",
        _ => $"Je draait de applicatie in de {env.EnvironmentName}-omgeving."
    };
    return Results.Ok(new { message });
});

app.MapControllers(); // laat staan als je ook controllers hebt
app.Run();
```

---

## 4) Test je environments

1. **Kies profiel** in Visual Studio (dropdown naast de ▶️ startknop).  
2. Run de API en navigeer naar:  
   - `https://localhost:7146/environment-message` (poorten kunnen verschillen)
3. **Verwachte output** per omgeving:

**Development**
```json
{ "message": "Je draait de applicatie in de Development-omgeving." }
```

**Staging**
```json
{ "message": "Je draait de applicatie in de Staging-omgeving." }
```

**Production**
```json
{ "message": "Je draait de applicatie in de Production-omgeving." }
```

---

## 5) Snelle check & valkuilen

### ✅ Checklist
- [ ] 3 environment-bestanden aanwezig (`appsettings.Development.json`, `appsettings.Staging.json`, `appsettings.Production.json`) — **optioneel** voor deze oefening, maar goed om te tonen.
- [ ] 3 profielen in `launchSettings.json` met **exacte** `ASPNETCORE_ENVIRONMENT`-waarden.
- [ ] Endpoint `/environment-message` bestaat en retourneert **exacte** teksten.
- [ ] Project **buildt** en start, zonder overbodige packages of code.

### ⚠️ Valkuilen
- ❌ Verkeerde of afwijkende teksten → **puntenverlies**. Gebruik exact de zinnen hierboven.
- ❌ Typo in omgeving (`production` i.p.v. `Production`) → wrong file/override.

---

# 🧭 Oefening 3 — Routing (met Services, Interface en Controller)

> In deze oefening maak je een **volledige routingstructuur** met services, een interface en dependency injection.  
> Je leert **hoe route constraints en binding sources werken**, en hoe je `Program.cs` configureert.  

---

## 🧩 Stappenplan

1. **Modelklasse Configureren** → in `Models/`  
2. **Interface Configureren** → in `Services/`  
3. **Serviceklasse Configureren** → in `Services/` (bv. `ProductsService`)  
4. **Controller Configureren** → in `Controllers/` (bv. `ProductsController`)  
5. **Program.cs** configureren met **dependency injection** en eventueel **lowercase URLs**  
6. **Testen in Swagger of browser**

> 👉 Voor de volledige uitwerking (met data en logica) kun je het voorbeeld **“Routing”** van **Digitap** downloaden.  
> Hieronder zie je de belangrijkste configuraties en uitleg over **constraints** en **binding sources**.

---

## ⚙️ Stap 1 — Program.cs configureren

In `Program.cs` registreer je je service via **dependency injection**  
zodat de controller toegang heeft tot de service.

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection: interface koppelen aan implementatie
builder.Services.AddSingleton<IProductsService, ProductsService>();

// (optioneel) URLs automatisch lowercase maken
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
```

---

## 🚏 Stap 2 — Route Constraints (Beperkingen in je route)

**Wat zijn constraints?**  
Route constraints zorgen ervoor dat **parameters in een route aan een bepaald type of patroon moeten voldoen**.  
Zo voorkom je dat een verkeerde waarde in je URL een fout veroorzaakt.

### 📘 Basisvorm:
```
{parameterNaam:typeConstraint}
```

### 📋 Veelgebruikte constraints:

| Type | Voorbeeld | Betekenis |
|------|------------|------------|
| `int` | `{id:int}` | Alleen gehele getallen toegestaan |
| `string` | `{name:string}` | Alleen tekstwaarden toegestaan |
| `bool` | `{flag:bool}` | Alleen `true` of `false` |
| `decimal` | `{price:decimal}` | Getallen met decimalen |
| `guid` | `{guid:guid}` | Alleen geldige GUIDs |
| `min` / `max` | `{age:min(18)}` | Minimaal 18 |
| `length` | `{code:length(5)}` | Lengtebeperking |
| `range` | `{id:range(1,100)}` | Lengtebeperking |

---

### 💡 Voorbeeld — route constraint op `int`

```csharp
// GET /api/products/{id:int}/details
[HttpGet("{id:int}/details")]
public async Task<ActionResult<Product>> GetProduct(int id)
{
    var product = await _productsService.GetProductById(id);
    if (product == null)
    {
        return NotFound();
    }
    return Ok(product);
}
```

🧠 **Uitleg:**  
De `:int` constraint zorgt ervoor dat `/api/products/abc/details` niet geldig is.  
Alleen `/api/products/2/details` zal werken.

---

## 🎯 Stap 3 — Binding Source Attributes

**Wat is source binding?**  
Binding sources vertellen ASP.NET **waar een parameter vandaan komt** (body, route, query, header, etc).  
Hierdoor weet de API hoe ze de inkomende data moet koppelen aan parameters.

### 📋 Veelgebruikte attributen

| Attribuut | Bron | Voorbeeld |
|------------|------|------------|
| `[FromBody]` | JSON-body van de request | `POST`, `PUT` requests |
| `[FromRoute]` | Padsegment van de URL | `/api/products/1/details` |
| `[FromQuery]` | Querystring na `?` | `/api/products/search?name=laptop` |
| `[FromHeader]` | HTTP-header | custom metadata |
| `[FromForm]` | Form-data (multipart) | file uploads |

---

### 💡 Voorbeeld — `[FromQuery]` gebruiken

```csharp
// GET /api/products/search?name={name}
[HttpGet("search")]
public async Task<ActionResult<List<Product>>> SearchProductsByName([FromQuery(Name = "name")] string name)
{
    var products = await _productsService.SearchProductsByName(name);
    if (products == null || !products.Any())
    {
        return NotFound();
    }
    return Ok(products);
}
```

🧠 **Uitleg:**  
- `[FromQuery]` haalt de waarde van de querystring (`?name=...`) uit de URL.  
- `(Name = "name")` maakt de binding expliciet, zodat de parameter exact overeenkomt.  
- Zonder dit attribuut zou ASP.NET proberen `name` uit het pad (`route`) te halen — wat hier niet lukt.

---

## 🧩 Extra voorbeeld — gecombineerde constraint en query

```csharp
// GET /api/products/category/{categoryName:string}/price/{minPrice:decimal}-{maxPrice:decimal}
[HttpGet("category/{categoryName:string}/price/{minPrice:decimal}-{maxPrice:decimal}")]
public async Task<ActionResult<List<Product>>> GetProductsByCategoryAndPrice(
    string categoryName, decimal minPrice, decimal maxPrice)
{
    var products = await _productsService.GetProductsByCategoryAndPrice(categoryName, minPrice, maxPrice);
    if (products == null || !products.Any())
    {
        return NotFound();
    }
    return Ok(products);
}
```

💡 **Uitleg:**  
- Hier combineer je **meerdere constraints** in één route.  
- `{categoryName:string}` zorgt voor een tekstuele categorie.  
- `{minPrice:decimal}-{maxPrice:decimal}` definieert een **prijsvork** direct in het pad (geen querystring!).  
  Voorbeeld-URL:  
  ```
  /api/products/category/Electronics/price/50-500
  ```

---

## ✅ Samenvatting

| Onderdeel | Wat het doet | Voorbeeld |
|------------|--------------|------------|
| **Dependency Injection** | Service koppelen aan interface | `builder.Services.AddSingleton<IProductsService, ProductsService>();` |
| **Lowercase URLs** | URLs consistent in kleine letters | `builder.Services.AddRouting(options => options.LowercaseUrls = true);` |
| **Route Constraints** | Type afdwingen in padparameters | `[HttpGet("{id:int}")]` |
| **Binding Sources** | Data ophalen uit query, route, body... | `[FromQuery]`, `[FromRoute]`, `[FromBody]` |

---

## 🧪 Testtips

- Test in **Swagger** of met **browser/Insomnia/Postman**.  
- Controleer of:
  - Je route exact overeenkomt met de vraag (`/api/products/{id:int}/details` enz.).  
  - De juiste HTTP-methoden gebruikt zijn (`GET`, `POST`, `PUT`, `DELETE`).  
  - Je constraints correct werken (foute types geven 404).  
  - Querystrings en body’s correct gebonden worden.

> ✔️ Als alles werkt, heb je routing, dependency injection, constraints en binding volledig onder de knie! 🚀
