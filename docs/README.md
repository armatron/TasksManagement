<!-- not include in TOC -->
<h1 id="introduction">Upravljanje z zahtevki</h1>

Vsebina:
- [Uvod](#uvod)
- [Tehnologije](#tehnologije)
- [Implementacija](#implementacija)
- [Struktura projekta](#struktura-projekta)
- [Zagon aplikacije iz VS](#zagon-aplikacije-iz-vs)
- [Objava aplikacije na WEB strežnik](#objava-aplikacije-na-web-strežnik)
- [Zagon aplikacije iz dockerja (Docker Desktop)](#zagon-aplikacije-iz-dockerja-docker-desktop)





## Uvod

WEB API omogoča osnovno funckionalnost zahtevkov. Zahtevki se shranjujejo v InMemory podatkovno bazo. Vsebuje eno entiteto Tasks z naslednimi parametri: Id, Title, Description, IsCompleted, DueDate, Priority, CreatedDate, UpdatedDate.

## Tehnologije
1. WEB API projekt z .NET Core v8 framework
2. Nuget paketi Serilog, ki omogočajo konfiguracijo in zajem ter zapisovanje izjem v datoteko
3. Nuget paket MediatR, ki omogoča implementacijo CQRS vzorca (commands and queries)

## Implementacija
1. Projekt ima ločene sloje za domeno, aplikacijo, infrastrukturo in vmesnik. implementirani sloji:
    1. Domenski sloj je v projektu TasksManagement.Domain. Vsebuje entiteto (objekt) z njegovimi parametri in vmesnike za CRUD metode.
    2. Aplikacijski sloj je v projektu TasksManagement.Infrastructure. Vsebuje kontekst podatkovne baze v povezavi z EF, CRUD metode ter Nuget paket za EF InMemory bazo. ima referenco do domenskega sloja.
    3. Infrastrukturni sloj je v projektu TasksManagement.Application. Vsebuje komande (dodajanje, spreminjanje, brisanje) in querije (poizvedbe) za CRUD metode ter Nuget paket MediatR za implementacijo vzorca mediator. Ima referenco do aplikacijskega sloja
    4. Predstavitveni sloj je v projektu TasksManagement.Api. Vsebuje kontrole (Get, Post, Put, Delete) in logiranje izjem. Ima Nuget pakete za ASP.NET Core, EF, Swagger za testiranje API-ja ter Serilog za podporo logiranja. Ima referenco do infrasatrukturnega sloja ter aplikacijskega sloja.


2. Za logiranje se uporabljajo naslednji Nuget paketi:
    1. Serilog.AspNetCore za podporo logiranja, 
    2. Serilog.Sinks.File za pisanje logov v datoteko, 
    3. Serilog.Settings.Configuration za kofiguriranje Seriloga ter 
    4. Serilog.Extensions za možnost filtriranja nivojev logiranja. 

    Aplikacija v dveh ločenih datotekah logira izjeme in zahtevke. Narejena je tudi podpora za logiranje zahtevkov in odgovorov z Middleware, vendar je zaradi težav pri odgovoru v Swaggerju onemogočena.

3. Relacije med posamezniki sloji so definirani z razredi DependencyInjection, ki so vključeni v aplikacijskem sloju. V aplikacijskem sloju je v razredu DependencyInjection vključen tudi MeriatR.

4. Entiteta Tasks ima določene posebnosti:
    1. Primarni ključ (Id) je tipa GIUD, ki se samodejno določi pri dodajanju etitete v bazo
    2. Datum nastanka entitete (CreatedDate) se samodejno določi pri dodajanju entitete v bazo
    3. Datum zadnje posodobitve entitete (UpdatedDate) se samodejno določi pri spremembi entitete v bazi. Pri dodajanju entitete v bazo je vrerndost null.

        Te izjeme so določene v infrastrukturnem sloju v CRUD metodah (metodi za dodajanje in spreminjanje). Ostale možnosti:
        * določitev z oznakami v samem modelu (domenski sloj) (težave zaradi InMemory baze).
        * override metode OnModelCreating (zakomentirano v AppDbContext, težave zaradi InMemory baze)
        * override metode SaveChangesAsync (zakomentirano v AppDbContext),         

## Struktura projekta
V projektu so naslednje datoteke in mape:
1. docs\: mapa z README.med,
2. logs\: mapa za log datoteke. Kreira se samodejno po zagonu aplikacije,
3. src\TasksManagement.Api: predstavitveni sloj aplikacije,
4. src\TasksManagement.Application: aplikacijsko sloj aplikacije,
5. src\TasksManagement.Domain: domenski sploj aplikacije,
6. src\TasksManagement.Infrastructure: infrastrukturni sloj aplikacije,
7. TasksManagement.sln: solution datoteka za Microsoft Visual Studio.
8. docker-compose.yml: datoteka uporabo docker image-a.


## Zagon aplikacije iz VS
1. V Visual Studio 2022 odpremo datoteko TasksManagement.sln
2. V meniju izberemo Debug \ Start without Debugging
3. Po prvem izvedenem zahtevku se na lokaciji, kjer se nahaja .sln datoteka kreira mapa logs, kamor se zapisujejo logi (zahtevki in izjeme).


## Objava aplikacije na WEB strežnik
Objava se izvede v dveh korakih: priprava izvršne kode v VS ter objava izvršner kode na WEB strežnik.
1. Priprava izvršne kode v VS (primer: https://www.c-sharpcorner.com/UploadFile/3d39b4/publish-and-host-Asp-Net-web-api/):
    1. V Visual Studio 2022 odpremo datoteko TasksManagement.sln
    2. V meniju izberemo Build \ Publish TasksManagement.Api
    3. V čarovniku za objavo izberemo Folder (priprava izvršne kode v datoteko)
    4. V naslednjem koraku izberemo pot, kamor se bodo shranila izvršna koda.
    5. Kliknemo gumb Publish. V izbrano mapo se dodajo datoteke izvršne kode.

2. Objava na WEB strežnik (primer https://www.c-sharpcorner.com/article/hosting-asp-net-web-api-rest-service-on-iis-10/):
    1. Datoteke je potrebno presnesti na spletni strežnik ter v IIS določiti nastavitve za zagon projekta.


## Zagon aplikacije iz dockerja (Docker Desktop)
Zagon se izvede v dveh korakih: priprava docker image-a in zagon docker image-a:
1. Priprava docker image-a:
    1. Zaženemo Docker Desktop
    2. V Visual Studio 2022 odpremo datoteko TasksManagement.sln
    3. V meniju izberemo View \ Terminal
    4. V Terminal vpišemo "docker-compose build". S tem se v Docker Desktop ustvari docker image "tasksmanagementapi" z oznako "latest".
    5. V Terminal vpišemo  "docker save -o publish/tasksmanagementapi.tar tasksmanagementapi:latest". S tem se na v mapo "publish" shrani docker image.

2. Zagon docker image-a v Docker Desktop:
    1. Datoteko publish/tasksmanagementapi.7z razpakiramo. Dobimo datoteko tasksmanegementapi.tar.
    2. Za zagon iz Docker Desktop zaženemo "Comand Prompt". Z ukazon "cd" se pomaknemo na lokacijo datoteke "tasksmanegementapi.tar" ter poženemo "docker load -i tasksmanagementapi.tar". S tem se v Docker Deskop ustvari image "tasksmanagementapi" z oznako "latest".
    3. Nato v Terminal vpišemo "docker-compose up". S tem se v Docker Desktop ustvari in zažene kontejner "tasksmanagement".
    4. V brskalnik vpišemo: "https://localhost:5001/swagger/index.html" (port 5001 je definiran v decker-compose.yaml)
