# Miinaharava

### Pelin kuvaus
Miinaharava on klassinen peli, jossa tarkoituksena on paljastaa kaikki ruudut pelilaudalta ilman että osuu miinaan. Pelaaja voittaa, kun kaikki miinattomat ruudut on paljastettu. Jos pelaaja osuu miinaan, peli päättyy häviöön.

### Pelin toiminta
Peli käynnistyy valitsemalla vaikeustaso (Helppo, Normaali, Vaikea) ja painamalla "Play" -nappia. Riippuen valitusta vaikeustasosta, pelilaudan koko ja miinojen määrä vaihtelevat.

#### Vaikeustasot:
  - Helppo: 9x9 ruudukko, 10 miinaa
  - Normaali: 16x16 ruudukko, 40 miinaa
  - Vaikea: 30x16 ruudukko, 99 miinaa

Pelin aikana voit klikata ruutuja paljastaaksesi ne. Jos ruutu sisältää miinan, peli päättyy häviöön ja kaikki miinat paljastetaan. Tyhjät ruudut paljastavat vierekkäiset miinattomat ruudut automaattisesti.

Pelaaja voi myös asettaa lipun ruutuun oikealla hiiren painikkeella ilmaistakseen epäilynsä miinasta. Voittaminen edellyttää kaikkien miinattomien ruutujen paljastamista.

### Ohjelmakoodi
Pelin logiikka perustuu C#-ohjelmointikieleen ja käyttää Windows Formsia pelin käyttöliittymänä. Pelilaudan generointi, miinojen sijoittaminen ja ruutujen tulostaminen ovat keskeisiä toimintoja, jotka on toteutettu ohjelmakoodissa.

## Asennus ja käyttöönotto
### 1. Ohjelmointiympäristö:
  - Suositeltavaa käyttää Visual Studioa tai vastaavaa C#-kehitysympäristöä.

### 2. Lataus ja käynnistys:
  - Lataa lähdekoodi GitHubista.
  - Avaa koodi kehitysympäristössä ja käännä se.
  - Suorita ohjelma ja valitse vaikeustaso.
  - Pelaa klikkaamalla ruutuja ja asettamalla lippuja miinoihin.

## Kehittäjä
Tämä Miinaharava-peli on kehitetty C#-ohjelmointikielellä käyttäen Windows Formsia käyttöliittymään. Koodin kehittäjä on vastuussa pelin toiminnasta ja ylläpidosta.

### Lisenssi
Tämä peli on avoimen lähdekoodin ohjelmisto, lisensoitu MIT-lisenssillä. Voit vapaasti käyttää, muokata ja jakaa tätä ohjelmakoodia.
