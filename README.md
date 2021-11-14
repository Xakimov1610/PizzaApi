# PizzaApi

## **Ilmhub Pizza**

- [x]  API Created
- [x]  Pizza Entity
- [x]  Pizza Model
- [x]  Migrations
- [x]  Pizza Controller
- [x]  Database

### Na'muna holat

Tasavvur qiling siz `Ilmhub Pizza` pitsa do'konlari tarmog'ida dasturiy ta'minotchi bo'lib ishlaysiz. Gap shundaki, yaqin orada `Ilmhub Pizza` Telegram Bot, Veb-sayt va Mobil ilova kabi o'z klient dasturlariga ega bo'ladi. Sizga tarmoqdagi barcha pitsa turlari omborini veb-servis sifatida qurish va klient dasturlarni ma'lumot bilan ta'minlash yuklatilgan.

### Topshiriq

[Asp.Net](http://asp.net/) Core orqali yangi web Api proyekt yarating. Proyektda ma'lumotlar ombori uchun SQL Serverdan foydalaning. Tarmoqdagi pitsa turlarini yaratish, o'zgartirish, o'chirish va taqdim etish uchun WebApi Controller actionlar yarating. ✅

### Pizza entity

`/Entities/Pizza.cs`

- **Id**: Guid → key, konstruktorda qiymat beriladi ✅
- **Title**: string → maksimum 255 belgidan iborat, maksimum 255 belgi, majburiy property (bo'sh bo'lishi mumkin emas), *unique emas* ✅
- **ShortName**: string → uzunligi 3 ga teng bo'lgan majburiy property, *unique* (masalan: Pepperoni → PPR) ✅
- **StockStatus**: EPizzaStockStatus (enum) → [In, Out] majburiy ✅
    - Ushbu property uchun `/Entities` papkasida alohida enum yaratish kerak ✅
- **Ingredients**: string → (vergul bilan ajratilgan masallig'lar ro'yhati) maksimum 1024 belgi, majburiy property (bo'sh bo'lishi mumkin emas) ✅
- **Price**: double → majburiy property, minimum qiymati 0 va maksimum qiymati 1000 ✅

### Pizza model

`/Models/Pizza.cs`

- **Id**: Guid → key ✅
- **Title**: string → maksimum 255 belgidan iborat, maksimum 255 belgi, majburiy property (bo'sh bo'lishi mumkin emas), *unique emas* ✅
- **ShortName**: string → uzunligi 3 ga teng bo'lgan majburiy property, *unique* (masalan: Pepperoni → PPR) ✅
- **StockStatus**: EPizzaStockStatus (enum) → [In, Out] majburiy ✅
    - Ushbu property uchun `/Models` papkasida alohida enum yaratish kerak ✅
- **Ingredients**: string → (vergul bilan ajratilgan masallig'lar ro'yhati) maksimum 1024 belgi, majburiy property (bo'sh bo'lishi mumkin emas) ✅
- **Price**: double → majburiy property, minimum qiymati 0 va maksimum qiymati 1000 ✅

### Pizza Controller

Qisqacha ko'rinishi.

```
[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
		private readonly IStoreService _pizzaStore;
		private readonly ILogger<PizzaController> _logger;

		public PizzaController(...) { ... }

		// GET - hamma pitsa turlarini qaytaradi

		// GET - idsiga qarab pitsa qaytaradi

		// POST - yangi pitsa turini yaratadi

		// PUT - berilgan pitsani o'zgartiradi

		// DELETE - berilgan idga ega pitsani o'chirib yuboradi
}

```

Controllerga qabul qilingan hamma Pizza ma'lumotlar `/Models/Pizza.cs` modelga parse qilinishi kerak. Qaytarib klientga jo'natilayotgan ma'lumotlar ham ushbu modelga parse qilinishi kerak. ✅

- Userdan ma'lumot qabul qilganda, Validation `/Models/Pizza.cs` ichiga yozilga attributelar orqali amalga oshirilishi kerak.
- enum tipdagi propertylar qaytarilganda 'string' ko'rinishida bo'lishi kerak. ✅

### Action detallari

## `GET` - `/api/pizza` ✅

```
[HttpGet]
public async Task<ActionResult> GetAllAsync() { ... }

```

Qaytariladigan ma'lumotlar

`Ok()` ✅

**200**

Barcha pitsa turlari response tanasida `/Models/Pizza.cs` JSON Array shaklida yuboriladi ✅

---

## `GET` - `/api/pizza/{id}` ✅

```
[HttpGet]
[Route("{id}")]
public async Task<ActionResult> GetAsync([FromRoute]Guid id) { ... }

```

Qaytariladigan ma'lumotlar

`Ok()` ✅

**200**

Berilgan `id`ga mos keluvchi predmet ma'lumotlar omborida bor bo'lsa, response tanasida `/Models/Pizza.cs` JSON shaklida yuboriladi

`NotFound()`

**404**

Berilgan `id`ga mos keluvchi predmet ma'lumotlar omborida topilmasa qaytariladi (ixtiyoriy response error message jo'natsa bo'ladi). ✅

---

## `POST` - `/api/pizza`

```
[HttpPost]
public async Task<ActionResult> CreateAsync([FromBody]Models.Pizza pizza)
{
		 ...
		return CreatedAtAction(nameof(CreateAsync), new {id = pizzaEntity.Id }, pizzaEntity);

		// pizzaEntity - bu ma'lumotlar omboriga endigina qo'shilgan pizza predmet
}

```

Qaytariladigan ma'lumotlar

`CreatedAtAction()`

**201**

Hozirgina ma'lumotlar omboriga qo'shilgan Pizza Entity response bodyda yuqorida ko'rsatilganday JSON shaklida qaytariladi.

`BadRequest()`

**400**

Request (so'rov) tanasida yuborilgan pitsa ma'lumotlar hato (invalid) bo'lganida qaytariladi ✅

---

## `PUT` - `/api/pizza/{id}`

```
[HttpPut]
[Route("{id}")]
public async Task<ActionResult> CreateAsync([FromRoute]Guid id, [FromBody]Models.Pizza pizza)
{
		 ...
}

```

Qaytariladigan ma'lumotlar

`NoContent()`

**204**

Kerakli pitsa predmeti o'zgartirildi. Tanasi bo'sh bo'lgan response qaytariladi.

`BadRequest()`

**400**

Request (so'rov) tanasidagi Pizza objecting **Id** propertysi bilan route-parameter **id** bir biriga mos kelmaganida qaytariladi. 

`BadRequest()`

**400**

Request (so'rov) tanasidagi Pizza object invalid bo'lganida qaytariladi (Model Validation) ✅

---

## `DELETE` - `/api/pizza/{id}` ✅

```
[HttpPut]
[Route("{id}")]
public async Task<ActionResult> CreateAsync([FromRoute]Guid id)
{
		 ...
}

```

Qaytariladigan ma'lumotlar

`NoContent()`

**204**

Kerakli pitsa predmeti o'chirildi. Tanasi bo'sh bo'lgan response qaytariladi.

`NotFound()`

**404**

Berilgan `id`ga mos keluvchi predmet ma'lumotlar omborida topilmasa qaytariladi (ixtiyoriy response error message jo'natsa bo'ladi). ✅