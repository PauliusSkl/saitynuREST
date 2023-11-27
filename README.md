# Forumas

# Sistemos paskirtis

Projekto tikslas - leisti žmonėms kurti temas, temose ivarius irašus bei tuos įrašus aptarti komentarais.

Veikimo principas - platforma sudarys dvi dalys: internetinė aplikacija, kuria
naudosis naudotojai ir administratorius bei aplikacijų programavimo sąsaja (angl. trump. API).

Naudotojai gales prisiregistruoti, prisijungti, tada sukurti ivarias temas arba įrašus egzistuojančiuose 
temose bei komentuoti po esamais įrašais. Administratorius moderuotu forumą, galėtu ištrinti visas temas, įrašus, komentarus. Valdyti naudotojus.

## Funkciniai reikalavimai

### API metodai:

- Kurti/skaityti/keisti/trinti temą ir gauti visų temų sarašą.
- Kurti/skaityti/keisti/trinti įrašą ir gauti visų įrašų sarašą.
- Kurti/skaityti/keisti/trinti komentarą ir gauti visų komentarų sarašą.

### Rolės:

- Svečias
- Administratorius
- Registruotas naudotojas

### Neregistruotas/neprisijungias sistemos naudotojas:

1. Prisijungti/užsiregistruoti prie platformos.
2. Peržiūrėti temas ir įrašus.

### Registruotas/prisijungias sistemos naudotojas:

1. Atsijungti
2. Sukurti temą.
3. Sukurti įrašą temoje.
4. Parašyti komentarą po įrašu.
5. Peržiūrėti visas temas.
6. Peržiūrėti visus temos įrašus.
7. Peržiūrėti visus įrašo komentarus.
8. Peržiūrėti kitų naudotojų informaciją.
9. Šalinti savo sukurtas temas, įrašus, komentarus.

### Administratorius galės:

1. Šalinti bet kokia temą, įrašą, komentarą.
2. Šalinti naudotojus.

## Technologijos

Front: React  
Back: .NET 7 + MySqlServer 

## Sistemos architektūra


![image](https://github.com/PauliusSkl/saitynuREST/assets/99750713/a8a50bcb-5698-471d-9237-79e4fd3d87ba)


# API specifikacija

### GET api/topics

Grąžina temas. Temos gražinamos po dvi puslapiais. Rezultatai gali būti naviguojami su `pageNumber` arba puslapio dydis padidinamas su `pageSize`

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>No</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | topic id | | 1|
| name |  | topic name| | topic|
| description |  | topic description| | topic about animals|
| creationDate |  | topic created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
GET https://walrus-app-2r2tj.ondigitalocean.app/api/topics
```

### Atsakas

```http
Status 200
[
    {
        "id": 2,
        "name": "Topic1",
        "description": "First topic",
        "creationDate": "2023-10-17T15:16:45.64501Z"
    },
    {
        "id": 3,
        "name": "Topic2",
        "description": "Second topic",
        "creationDate": "2023-10-17T17:31:26.546829Z"
    }
]
```



