# The Chosen Store

Welcome to The Chosen Store, a comprehensive Web Shop application featuring an Administration Area, Discount Management, Inventory Management, and Authentication & Authorization functionalities.

## Features

- Manage products, orders, inventory, and discounts
- Share articles and allow customer comments
- Online and on-site payment options for customers
- Responsive front-end using Vanilla JS, jQuery, and AJAX

## Installation

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine
- [Entity Framework Core tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) for managing database migrations

### Getting Started

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/your-repository.git
   ```

2. Navigate to the project directory:

   ```bash
   cd TheChosenStore
   ```

3. Update EF Core database:

   ```bash
   dotnet ef database update
   ```

   This command will apply any pending migrations and update the database schema according to your model changes.

4. Run the application:

   ```bash
   dotnet run
   ```

   Visit `https://localhost:5001` in your browser to access the application.

## Contributing

Contributions are welcome! Feel free to submit pull requests or open issues for any suggestions or improvements.

## License

This project is licensed under the [MIT License](LICENSE).
