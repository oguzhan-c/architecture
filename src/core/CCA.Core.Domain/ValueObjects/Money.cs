
using CCA.Core.Domain.Primitives.Classes;

namespace CCA.Core.Domain.ValueObjects;

/// <summary>
/// Represents a monetary amount with an ISO 4217 currency code.
/// Immutable value object — all operations return a new <see cref="Money"/> instance.
/// </summary>
/// <remarks>
/// Decimal places are automatically determined by the currency code:
/// <list type="bullet">
///   <item>0 decimal places: JPY, KRW, VND, BIF, GNF</item>
///   <item>3 decimal places: KWD, BHD, OMR, TND</item>
///   <item>2 decimal places: all others (USD, EUR, TRY, GBP, etc.)</item>
/// </list>
/// Arithmetic across different currencies is not allowed and throws
/// <see cref="InvalidOperationException"/>.
/// </remarks>
public sealed class Money : ValueObject
{
    /// <summary>
    /// Gets the monetary amount, rounded to the correct decimal places
    /// for the currency.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the ISO 4217 currency code (e.g. "USD", "EUR", "TRY").
    /// Always stored in uppercase.
    /// </summary>
    public string CurrencyCode { get; }

    private Money(decimal amount, string currencyCode)
    {
        Amount = Math.Round(amount, GetDecimalPlaces(currencyCode));
        CurrencyCode = currencyCode.ToUpperInvariant();
    }

    /// <summary>
    /// Creates a new <see cref="Money"/> instance with the specified amount and currency.
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currencyCode">The ISO 4217 currency code.</param>
    /// <returns>A new <see cref="Money"/> instance.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="currencyCode"/> is null or whitespace.
    /// </exception>
    public static Money Of(decimal amount, string currencyCode)
    {
        Guard.NullOrWhiteSpace(currencyCode, nameof(currencyCode));
        return new Money(amount, currencyCode);
    }

    /// <summary>
    /// Creates a zero-amount <see cref="Money"/> instance for the specified currency.
    /// </summary>
    /// <param name="currencyCode">The ISO 4217 currency code.</param>
    /// <returns>A new <see cref="Money"/> instance with zero amount.</returns>
    public static Money Zero(string currencyCode) => Of(0, currencyCode);

    /// <summary>
    /// Adds another <see cref="Money"/> amount to this instance.
    /// Both amounts must have the same currency.
    /// </summary>
    /// <param name="other">The amount to add.</param>
    /// <returns>A new <see cref="Money"/> instance with the combined amount.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the currencies do not match.
    /// </exception>
    public Money Add(Money other)
    {
        GuardSameCurrency(other);
        return new Money(Amount + other.Amount, CurrencyCode);
    }

    /// <summary>
    /// Subtracts another <see cref="Money"/> amount from this instance.
    /// Both amounts must have the same currency.
    /// </summary>
    /// <param name="other">The amount to subtract.</param>
    /// <returns>A new <see cref="Money"/> instance with the reduced amount.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the currencies do not match.
    /// </exception>
    public Money Subtract(Money other)
    {
        GuardSameCurrency(other);
        return new Money(Amount - other.Amount, CurrencyCode);
    }

    /// <summary>
    /// Multiplies this amount by the specified factor.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>A new <see cref="Money"/> instance with the multiplied amount.</returns>
    public Money Multiply(decimal factor) =>
        new(Amount * factor, CurrencyCode);

    /// <summary>
    /// Applies a percentage discount to this amount.
    /// </summary>
    /// <param name="percentage">The discount percentage (0–100).</param>
    /// <returns>A new <see cref="Money"/> instance with the discounted amount.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="percentage"/> is not between 0 and 100.
    /// </exception>
    public Money ApplyDiscount(decimal percentage)
    {
        Guard.OutOfRange((int)percentage, nameof(percentage), 0, 100);
        return new Money(Amount * (1 - percentage / 100), CurrencyCode);
    }

    /// <summary>Gets a value indicating whether this amount is zero.</summary>
    public bool IsZero => Amount == 0;

    /// <summary>Gets a value indicating whether this amount is greater than zero.</summary>
    public bool IsPositive => Amount > 0;

    /// <summary>Gets a value indicating whether this amount is less than zero.</summary>
    public bool IsNegative => Amount < 0;

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return CurrencyCode;
    }

    /// <inheritdoc/>
    public override string ToString() => $"{Amount} {CurrencyCode}";

    private void GuardSameCurrency(Money other)
    {
        if (CurrencyCode != other.CurrencyCode)
            throw new InvalidOperationException(
                $"Cannot operate on {CurrencyCode} and {other.CurrencyCode}.");
    }

    private static int GetDecimalPlaces(string code) =>
        code.ToUpperInvariant() switch
        {
            "JPY" or "KRW" or "VND" or "BIF" or "GNF" => 0,
            "KWD" or "BHD" or "OMR" or "TND"          => 3,
            _                                          => 2
        };
}