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
![image](https://github.com/PauliusSkl/saitynuREST/assets/99750713/1e7d8d38-4cbf-4497-bc59-6aec7a4f1a5e)


# Naudotojo sąsaja

## Temų puslapio wireframe

![Topic](https://github.com/PauliusSkl/saitynuREST/assets/99750713/0324f000-24be-45bd-9558-77bb58aa74e2)

## Temų puslapis

![946be42243a8ed9d8ebf8c1410f4dc8a](https://github.com/PauliusSkl/saitynuREST/assets/99750713/f1b26441-f35a-4689-81a2-fadc519efd74)

## Įrašo/Komentaro puslapio wireframe

![comment](https://github.com/PauliusSkl/saitynuREST/assets/99750713/75e8bfe1-8472-4442-ba95-741f3c94fec8)

## Įrašo/Komentaro puslapis

![ecfb23a39932e9ca88753cced5ad9283](https://github.com/PauliusSkl/saitynuREST/assets/99750713/6644fac6-d9f6-408c-8b0e-2cd86bce1b78)



# API specifikacija

## Temos

## GET api/topics

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

## GET api/topics/:id

Grąžina specifinę tema kaip `resource` ir `links` kurie parodo, ką galima atlikti su resursu.

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
GET https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2
```

### Atsakas

```http
Status 200
{
    "resource": {
        "id": 2,
        "name": "Topic1",
        "description": "First topic",
        "creationDate": "2023-10-17T15:16:45.64501Z"
    },
    "links": [
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2",
            "rel": "self",
            "method": "GET"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2",
            "rel": "DeleteTopic",
            "method": "DELETE"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2",
            "rel": "UpdateTopic",
            "method": "PUT"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts",
            "rel": "GetPosts",
            "method": "GET"
        }
    ]
```

## POST api/topics

Sukuria nauja temą.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | topic id | | 1|
| name | Yes | topic name| | topic|
| description | Optional | topic description| | topic about animals|
| creationDate |  | topic created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
POST https://walrus-app-2r2tj.ondigitalocean.app/api/topics
```
### Body
```http
{
    "Name" : "Damn",
    "Description" : "Intresting"
}
```

### Atsakas

```http
Status 200
{
    "id": 59,
    "name": "Damn",
    "description": "Intresting",
    "creationDate": "2023-11-28T17:05:29.7266917Z"
}
```
## PUT api/topics/:id

Atnaujina temos aprašą.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | topic id | | 1|
| name |  | topic name| | topic|
| description | Yes | topic description| | topic about animals|
| creationDate |  | topic created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
PUT https://walrus-app-2r2tj.ondigitalocean.app/api/topics/59
```
### Body
```http
{
    "Description" : "Updated description"
}
```

### Atsakas

```http
Status 200
{
    "id": 59,
    "name": "Damn",
    "Description" : "Updated description"
    "creationDate": "2023-11-28T17:05:29.7266917Z"
}
```
## DELETE api/topics/:id

Ištrina temą.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
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
DELETE https://walrus-app-2r2tj.ondigitalocean.app/api/topics/59
```

### Atsakas

```http
Status 204
```


## Įrašai

## GET api/topics/:topicId/posts

Grąžina specifinės temos įrašus. Įrašai gražinami po du, puslapiais. Rezultatai gali būti naviguojami su `pageNumber` arba puslapio dydis padidinamas su `pageSize`

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
| id |  | post id | | 8|
| name |  | post name| | post about topic|
| body |  | post body, content| | This topic is intresting |
| creationDate |  | post created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
GET https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts
```

### Atsakas

```http
Status 200
[
    {
        "id": 8,
        "name": "post1",
        "body": "post body",
        "creationDate": "2023-11-20T18:09:22.247045Z"
    },
    {
        "id": 10,
        "name": "post2",
        "body": "post body",
        "creationDate": "2023-11-20T18:09:24.857851Z"
    }
]
```

## GET api/topics/:topicId/posts/:postId

Grąžina specifinį įrašą kaip `resource` ir `links` kurie parodo, ką galima atlikti su resursu.

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
| id |  | post id | | 8|
| name |  | post name| | post about topic|
| body |  | post body, content| | This topic is intresting |
| creationDate |  | post created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
GET https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8
```

### Atsakas

```http
Status 200
{
    "resource": {
        "id": 8,
        "name": "post1",
        "body": "post body",
        "creationDate": "2023-11-20T18:09:22.247045Z"
    },
    "links": [
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8",
            "rel": "self",
            "method": "GET"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8",
            "rel": "DeletePost",
            "method": "DELETE"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8",
            "rel": "UpdatePost",
            "method": "PUT"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments",
            "rel": "GetComments",
            "method": "GET"
        }
    ]
}
```

## POST api/topics/:topicId/posts

Sukuria nauja įrašą temoje.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | post id | | 8|
| name | Yes | post name| | post about topic|
| body | Optional | post body, content| | This topic is intresting |
| creationDate |  | post created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
POST https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts
```
### Body
```http
{
    "Name" : "Funny joke",
    "Body" : "Joke body"
}
```

### Atsakas

```http
Status 200
{
    "id": 69,
    "name": "Funny joke",
    "body": "Joke body",
    "creationDate": "2023-11-29T08:59:41.1298579Z"
}
```
## PUT api/topics/:topicId/posts/:postId

Atnaujina įrašo turinį.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | post id | | 8|
| name |  | post name| | post about topic|
| body | Yes | post body, content| | This topic is intresting |
| creationDate |  | post created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
PUT https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/69
```
### Body
```http
{
    "body" : "New body"
}
```

### Atsakas

```http
Status 200
{
    "id": 69,
    "name": "Funny joke",
    "body": "New body",
    "creationDate": "2023-11-29T08:59:41.129857Z"
}
```
## DELETE api/topics/:topicId/posts/:postId

Ištrina įrašą.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | post id | | 8|
| name |  | post name| | post about topic|
| body | | post body, content| | This topic is intresting |
| creationDate |  | post created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
DELETE https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/69
```

### Atsakas

```http
Status 204
```

## Komentarai

## GET api/topics/:topicId/posts/:postId/comments

Grąžina specifinio temos įrašo komentarus. Komentarai gražinami po du, puslapiais. Rezultatai gali būti naviguojami su `pageNumber` arba puslapio dydis padidinamas su `pageSize`

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
| id |  | comment id  | | 8|
| content |  | Content of the comment| | I liked this |
| creationDate |  | Comment created date| | 2023-10-17T15:16:45.64501Z|

### Pavizdinė užklausa:
```http
GET https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments
```

### Atsakas

```http
Status 200
[
    {
        "id": 6,
        "content": "comment content1",
        "creationDate": "2023-11-23T20:31:47.659806Z"
    },
    {
        "id": 7,
        "content": "comment content2",
        "creationDate": "2023-11-23T20:32:16.431262Z"
    }
]
```

## GET api/topics/:topicId/posts/:postId/comments/:commentId

Grąžina specifinį komentarą kaip `resource` ir `links` kurie parodo, ką galima atlikti su resursu.

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
| id |  | comment id | | 8|
| content |  | Content of the comment| | I liked this |
| creationDate |  | Comment created date| | 2023-10-17T15:16:45.64501Z|


### Pavizdinė užklausa:
```http
GET https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments/6
```

### Atsakas

```http
Status 200
{
    "resource": {
        "id": 6,
        "content": "comment content1",
        "creationDate": "2023-11-23T20:31:47.659806Z"
    },
    "links": [
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments/6",
            "rel": "self",
            "method": "GET"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments/6",
            "rel": "UpdateComment",
            "method": "PUT"
        },
        {
            "href": "http://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments/6",
            "rel": "DeleteComment",
            "method": "DELETE"
        }
    ]
}
```

## POST api/topics/:topicId/posts/comments

Sukuria nauja komentarą įraše.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | comment id  | | 8|
| content | Yes | Content of the comment| | I liked this |
| creationDate |  | Comment created date| | 2023-10-17T15:16:45.64501Z|


### Pavizdinė užklausa:
```http
POST https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments
```
### Body
```http
{
    "Content": "I liked this"
}
```

### Atsakas

```http
Status 200
{
    "id": 79,
    "content": "I liked this",
    "creationDate": "2023-11-29T09:09:50.95084Z"
}
```
## PUT api/topics/:topicId/posts/:postId/comments/:commentId

Atnaujina komentaro turinį.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:

| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | comment id  | | 8|
| content | Yes | Content of the comment| | I liked this |
| creationDate |  | Comment created date| | 2023-10-17T15:16:45.64501Z|


### Pavizdinė užklausa:
```http
PUT https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments/79
```
### Body
```http
{
    "Content" : "New Content"
}
```

### Atsakas

```http
Status 200
{
    "id": 79,
    "content": "New Content",
    "creationDate": "2023-11-29T09:12:03.1274712Z"
}
```
## DELETE api/topics/:topicId/posts/:postId

Ištrina komentarą.

### Resurso informacija:

<table>
  <tr>
    <td>Response format</td>
    <td>JSON</td>
  </tr>
  <tr>
    <td>Requires authentication?</td>
    <td>Yes</td>
  </tr>
</table>

### Parametrai:


| Name | Required | Description | Default value | Example |
| --- | --- | --- | --- | --- |
| id |  | comment id | | 8|
| content |  | Content of the comment| | I liked this |
| creationDate |  | Comment created date| | 2023-10-17T15:16:45.64501Z|


### Pavizdinė užklausa:
```http
DELETE https://walrus-app-2r2tj.ondigitalocean.app/api/topics/2/posts/8/comments/79
```

### Atsakas

```http
Status 204
```


## Autorizacija

## POST api/register

Užregistruoja nauja naudotoją.

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
| id |  | user id | | 8|
| userName |  | Name that is used to login| | Tom1 |
| email |  | User email| | tom@gmail.com |
| password |  | Password that is used to login| | StrongPassword1! |


### Pavizdinė užklausa:
```http
GET https://walrus-app-2r2tj.ondigitalocean.app/api/register
```

### Body
```http
{
    "userName" : "Tom1",
    "email" : "tom@gmail.com",
    "password" : "StrongPassword1!"
}
```

### Atsakas

```http
Status 200
{
    "id": "3c3544fd-898a-4e4e-b247-5702792f74ba",
    "username": "Tom1",
    "email": "tom@gmail.com"
}
```

## POST api/topics/Login

Naudoto prisijungimas, gražina JWT tokeną kuris toliau naudojamas autorizacijai.

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
| id |  | user id | | 8|
| userName |  | Name that is used to login| | Tom1 |
| email |  | User email| | tom@gmail.com |
| password |  | Password that is used to login| | StrongPassword1! |

### Pavizdinė užklausa:
```http
POST https://walrus-app-2r2tj.ondigitalocean.app/api/login
```
### Body
```http
{
    "userName" : "Tom1",
    "password" : "StrongPassword1!"
}
```

### Atsakas

```http
Status 200
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGF1bGl1czYiLCJqdGkiOiI5YzYxODRhYS01YmNhLTQwZGYtOWY2My0zZDllNWFlZDFkOGMiLCJzdWIiOiJiZDk2ZmYwOS00ODAzLTQyZjQtYTQzNC0yNWI3ZTVlOGQ2MTgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJyZWdpc3RlcmVkVXNlciIsImV4cCI6MTcwMTI1MTk3OCwiaXNzIjoiUGF1bGl1cyIsImF1ZCI6IlRydXN0ZWRDbGllbnQifQ.rm2WnVO6OvLpjN1E5lTL6bfUBEU1S0qTsLhNP3LpT7w"
}
```


# Išvados
