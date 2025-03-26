public void ProcessOrder(Order order) 
{
    ValidateOrder(order);
    decimal total = CalculateTotal(order);
    ApplyDiscount(ref total, order.Customer);
    SaveOrder(order, total);
    SendConfirmationEmail(order);
}

private void ValidateOrder(Order order)
{
    if (order is null) 
        throw new ArgumentNullException(nameof(order));

    if (order.Items is null || order.Items.Count == 0) 
        throw new InvalidOperationException("Order cannot be empty.");
}

private decimal CalculateTotal(Order order)
{
    return order.Items.Sum(item => (item.Price * item.Quantity) + 
                                   (item.IsTaxable ? item.Price * 0.1m : 0));
}

private void ApplyDiscount(ref decimal total, Customer customer)
{
    if (customer?.IsPremium == true) 
    {
        total *= 0.9m; // 10% discount
    }
}

private void SaveOrder(Order order, decimal total)
{
    using var db = new AppDbContext();
    order.Total = total;
    db.Orders.Add(order);
    db.SaveChanges();
}

private void SendConfirmationEmail(Order order)
{
    var emailService = new EmailService();
    emailService.Send(order.Customer.Email, "Order Confirmed", $"Total: ${order.Total}");
}
