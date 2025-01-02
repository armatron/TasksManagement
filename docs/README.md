# Upravljanje z zahtevki

WEB API omogoča osnovno funckionalnost zahtevkov. Zahtevki se shranjujejo v InMemory podatkovno bazo. Vsebuje eno entiteto Tasks z naslednimi parametri: Id, Title, Description, IsCompleted, DueDate, Priority, CreatedDate, UpdatedDate.

# Tehnologija
1. WEB API projekt z .NET Core v8 framework
2. Nuget paketi Serilog, ki omogočajo konfiguracijo in zajem ter zapisovanje izjem v datoteko
3. Nuget paket MediatR, ki omogoča implementacijo vzorca mediator (commands and queries)

# Implementacija
1. Projekt ima ločene sloje za domeno, aplikacijo, infrastrukturo in vmesnik.
  * Domenski sloj je v projektu TasksManagement.Domain. Vsebuje entiteto (objekt) z njegovimi parametri in vmesnike za CRUD metode.
  * Aplikacijski sloj je v projektu TasksManagement.Infrastructure. Vsebuje kontekst podatkovne baze v povezavi z EF, CRUD metode ter Nuget paket za EF InMemory bazo. ima referenco do domenskega sloja.
  * Infrastrukturni sloj je v projektu TasksManagement.Application. Vsebuje komande (dodajanje, spreminjanje, brisanje) in querije (poizvedbe) za CRUD metode ter Nuget paket MediatR za implementacijo vzorca mediator. Ima referenco do aplikacijskega sloja
  * Predstavitveni sloj je v projektu TasksManagement.Api. Vsebuje kontrole (Get, Post, Put, Delete) in logiranje izjem. Ima Nuget pakete za ASP.NET Core, EF, za delo s kontajnerji, Swagger za testiranje API-ja ter Serilog za podporo logiranja. Ima referenco do infrasatrukturnega sloja ter aplikacijskega sloja.


2. Za logiranje se uporabvljajo klnjižnice Serilog.AspNetCore za podporo logiranja, Serilog.Sinks.File za pisanje logov v datoteko, Serilog.Settings.Configuration za kofiguriranje Seriloga ter Serilog.Extensions za možnost filtriranja nivojev logiranja. Aplikacija v dveh ločenih datotekah logira izjeme in zahtevke. Najrejena je tudi podpora za logiranje zahtevkov in odgovorov z Middleware, vendar je zaradi težav pri odgovoru v Swaggerju onemogočena.

3. Relacije med posamezniki sloji so definirani z razredi DependencyInjection, ki so vključeni v aplikacijskem sloju. V aplikacijskem sloju so v razredu DependencyInjection vključeni tudi MeriatR servisi.

4. Entiteta Tasks ima določene posebnosti:
  * Primarni ključ (Id) je tipa GIUD, ki se samodejno določi pri dodajanju etitete v bazo
  * Datum nastanka entitete (CreatedDate) se samodejno določi pri dodajanju entitete v bazo
  * Datum zadnje posodobitve entitete (UpdatedDate) se samodejno določi pri spremembi entitete v bazi. Pri dodajanju entitete v bazo je vrerndost null.

Te izjeme so določene v infrastrukturnem sloju v CRUD metodah (metodi za dodajanje in spreminjanje). 
Ostale možnosti:
* override metode SaveChangesAsync (zakomentirano v AppDbContext), 
* override metode OnModelCreating (zakomentirano v AppDbContext, težave zaradi InMemory baze)
* določitev z oznakami v samem modelu (domenski sloj) (težave zaradi InMemory baze).

# Struktura map in datotek
V projektu so naslednje datoteke in mape:
1. Docs: mapa z README.med
2. Logs: kreira se po zagonu aplikacije
3. TasksManagement.Api predstavitveni sloj aplikacije
4. TasksManagement.Application: aplikacijsko sloj aplikacije
5. TasksManagement.Domain: domenski sploj aplikacije
6. TasksManagement.Infrastructure: infrastrukturni sloj aplikacije
7. TasksManagement.sln: solution datoteka za Microsoft Visual Studio


# Zagon projekta iz VS
1. V Visual Studio 2022 odpremo datoteko TasksManagement.sln
2. V meniju izberemo Debug \ Start without Debugging
3. Po prvem izvedenem zahtevku se na lokaciji, kjer se nahaja .sln datoteka kreira mapa Logs, kamor se zapisujejo logi (zahtevki in izjeme).


# Objava projekta
Objava se izvede v dveh korakih: priprava izvršne kode v VS ter objava izvršner kode na WEB strežnik.
1. Priprava izvršne kode v VS (primer: https://www.c-sharpcorner.com/UploadFile/3d39b4/publish-and-host-Asp-Net-web-api/):
  * V Visual Studio 2022 odpremo datoteko TasksManagement.sln
  * V meniju izberemo Build \ Publish TasksManagement.Api
  * V čarovniku za objavo izberemo Folder (priprava izvršne kode v datoteko)
  * V naslednjem koraku izberemo pot, kamor se bodo shranila izvršna koda.
  * Kliknemo gumb Publish. V izbrano mapo se dodajo datoteke izvršne kode.
2. Objava na WEB strežnik (primer https://www.c-sharpcorner.com/article/hosting-asp-net-web-api-rest-service-on-iis-10/):
  * Datoteke je potrebno presnesti na spletni strežnik ter v IIS določit nastavitve za zagon projekta.

