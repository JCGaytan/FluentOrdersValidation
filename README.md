# FluentOrdersValidation

A RESTful API project built using **ASP.NET Core 9** and **FluentValidation** for robust and scalable data validation. This project demonstrates how to validate complex objects with nested properties using FluentValidation, including custom validation rules.

---

## Features

- **Customer Management**:
  - Validate `Customer` objects with properties like `FirstName`, `LastName`, `Email`, and `Order`.
- **Order Management**:
  - Validate `Order` objects, ensuring that the `OrderDate` is not in the future and that at least one product is included.
- **Product Management**:
  - Validate `Product` objects with rules for `Name` and `Price`.
- **Custom Validation Rules**:
  - Validate `Email` domains to allow only specific domains (e.g., `example.com` and `mycompany.com`).

---

## NuGet Packages Used

| Package                    | Description                                     |
|----------------------------|-------------------------------------------------|
| `FluentValidation.AspNetCore` | Middleware for integrating FluentValidation with ASP.NET Core. |
| `FluentValidation`         | A library for building strongly-typed validation rules. |

To install these packages:
```bash
dotnet add package FluentValidation.AspNetCore
dotnet add package FluentValidation
```

---

## Solution Structure

```
FluentOrdersValidation/
│
├── Models/
│   ├── Customer.cs
│   ├── Order.cs
│   └── Product.cs
│
├── Validators/
│   ├── CustomerValidator.cs
│   ├── OrderValidator.cs
│   └── ProductValidator.cs
│
├── Controllers/
│   └── CustomerController.cs
│
├── Program.cs
└── README.md
```

- **Models**: Contains the `Customer`, `Order`, and `Product` classes.
- **Validators**: Contains validation rules for each model using FluentValidation.
- **Controllers**: Handles the API endpoints for managing customers.

---

## Validation Rules

### **Customer Validation**
- `FirstName` and `LastName` are required.
- `Email` must:
  - Be a valid email address.
  - Have a domain that is allowed (`example.com`, `mycompany.com`).
- `Order` must be present and validated using the `OrderValidator`.

### **Order Validation**
- `OrderDate` must:
  - Be present.
  - Not be in the future.
- `Products` must:
  - Contain at least one product.
  - Each product is validated using the `ProductValidator`.

### **Product Validation**
- `Name` cannot be empty.
- `Price` must be greater than 0.

---

## How to Use

### **1. Clone the Repository**
```bash
git clone https://github.com/JCGaytan/FluentOrdersValidation.git
cd FluentOrdersValidation
```

### **2. Build and Run the Application**
```bash
dotnet build
dotnet run
```

The API will run at `https://localhost:7004`.

---

## API Endpoints

### **POST /api/customer**
Create a new customer with an order and products.

#### **Request Body Example**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "order": {
    "orderDate": "2024-12-25T10:00:00",
    "products": [
      { "name": "Laptop", "price": 1200 },
      { "name": "Mouse", "price": 20 }
    ]
  }
}
```

#### **Validation Errors Example**
```json
{
  "errors": {
    "Order.OrderDate": [
      "Order date cannot be in the future."
    ],
    "Order.Products[1].Price": [
      "Product price must be greater than zero."
    ]
  }
}
```

---

## How It Works

1. **FluentValidation Integration**:
   - Validators are automatically registered from the assembly using:
     ```csharp
     builder.Services.AddControllers()
         .AddFluentValidation(config =>
         {
             config.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
         });
     ```

2. **Nested Validation**:
   - `OrderValidator` and `ProductValidator` are called within `CustomerValidator` to ensure recursive validation.

3. **Custom Validation**:
   - A custom rule ensures the `Email` domain is in the allowed list:
     ```csharp
     private bool HasAllowedEmailDomain(string email)
     {
         var domain = email.Split('@')[^1];
         return _allowedEmailDomains.Contains(domain);
     }
     ```

---

## Testing the API

### **With Postman**
1. Create a new POST request to `https://localhost:7004/api/customer`.
2. Add the following JSON body:
   ```json
   {
     "firstName": "Jane",
     "lastName": "Doe",
     "email": "jane.doe@gmail.com",
     "order": {
       "orderDate": "2024-12-25T10:00:00",
       "products": [
         { "name": "Phone", "price": 700 },
         { "name": "", "price": -10 }
       ]
     }
   }
   ```
3. Observe the validation errors returned by the API.

### **With Curl**
```bash
curl -X POST https://localhost:7004/api/customer -H "Content-Type: application/json" -d '{
  "firstName": "Jane",
  "lastName": "Doe",
  "email": "jane.doe@gmail.com",
  "order": {
    "orderDate": "2024-12-25T10:00:00",
    "products": [
      { "name": "Phone", "price": 700 },
      { "name": "", "price": -10 }
    ]
  }
}'
```

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Contributions

Contributions are welcome! Feel free to submit issues or pull requests to improve this project.

---
