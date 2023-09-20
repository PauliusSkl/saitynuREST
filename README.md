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




![image](https://github.com/PauliusSkl/saitynuREST/assets/99750713/a9004d55-5765-4031-b7dd-647c228fec73)

