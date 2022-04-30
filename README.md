# LABB3-API
### Om uppgiften

I den här labben ska du testa att bygga ditt första enkla Webb-API. Det API du kommer konstruera använder en REST-arkitektur och kommer möjliggöra för externa tjänster och applikationer att hämta och ändra data i din egen applikation.

## API-ANROP

### Hämta

#### Hämta alla personer i databasen.

```
GET api/persons
```

#### Hämta alla intressen som är kopplade till specifik person. *Exempel: api/interests/rasmus*

```
GET api/interests/{name}
```

#### Hämta alla länkar som är kopplade till specifik person. *Exempel: api/links/linksbyname:rasmus*
```
GET api/links/linksbyname:{name}
```

### Skapa nytt

#### Koppla en person till ett nytt intresse.

I body´n skickar man med den valda personens ID samt det valda intressets ID.
```
POST api/interests
```

```
{"personId": 2, "interestId": 2}
```


#### Lägga in nya länkar för en specifik person och ett specifikt intresse. 

*Personen måste ha intresset för att man ska kunna lägga till en gemensam länk.*

*Exempel: api/links/rasmus/golf*
```
POST api/links/{personName}/{interestName}
```

```
Body: {"LinkUrl": "www.hudiksvallgolf.se"}
```





