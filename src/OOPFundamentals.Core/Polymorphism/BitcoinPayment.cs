namespace OOPFundamentals.Core.Polymorphism;

/// <summary>
/// Represents a Bitcoin cryptocurrency payment.
/// Demonstrates polymorphism with a modern, decentralized payment method.
/// Shows how emerging payment technologies can fit into existing payment frameworks.
/// </summary>
/// <remarks>
/// Bitcoin payments are fundamentally different:
/// - Decentralized blockchain verification
/// - No intermediary (bank, PayPal, etc.)
/// - Confirmation times vary
/// - Irreversible once confirmed
/// Yet still implements the same Payment interface!
/// </remarks>
public class BitcoinPayment : Payment
{
    #region Properties
    
    /// <summary>
    /// Gets or sets the Bitcoin wallet address.
    /// </summary>
    public string WalletAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the Bitcoin amount.
    /// </summary>
    public decimal BitcoinAmount { get; set; }
    
    /// <summary>
    /// Gets or sets the Bitcoin exchange rate at time of transaction.
    /// </summary>
    public decimal ExchangeRate { get; set; }
    
    /// <summary>
    /// Gets the blockchain transaction hash.
    /// </summary>
    public string? BlockchainTransactionHash { get; private set; }
    
    /// <summary>
    /// Gets the number of blockchain confirmations.
    /// </summary>
    public int Confirmations { get; private set; }
    
    /// <summary>
    /// Gets the required confirmations for completion (typically 3-6).
    /// </summary>
    public int RequiredConfirmations { get; set; } = 3;
    
    /// <summary>
    /// Gets whether the payment is confirmed on the blockchain.
    /// </summary>
    public bool IsConfirmed => Confirmations >= RequiredConfirmations;
    
    /// <summary>
    /// Gets or sets the blockchain network (Bitcoin, Lightning, etc.).
    /// </summary>
    public string Network { get; set; }
    
    /// <summary>
    /// Overrides the payment method name.
    /// </summary>
    public override string PaymentMethod => $"Bitcoin ({Network})";
    
    /// <summary>
    /// Overrides the transaction fee - Bitcoin has network fees (mining fees).
    /// Variable based on network congestion!
    /// </summary>
    public override decimal TransactionFeePercentage => 0.5m; // Simplified - actual fees vary
    
    /// <summary>
    /// Gets the Bitcoin network fee in BTC.
    /// </summary>
    public decimal NetworkFeeBTC => 0.0001m; // Example fee, varies with network
    
    /// <summary>
    /// Gets the masked wallet address.
    /// </summary>
    public string MaskedWalletAddress => 
        $"{WalletAddress.Substring(0, 6)}...{WalletAddress.Substring(WalletAddress.Length - 4)}";
    
    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Initializes a new instance of the BitcoinPayment class.
    /// </summary>
    /// <param name="amount">Payment amount in USD</param>
    /// <param name="walletAddress">Bitcoin wallet address</param>
    /// <param name="exchangeRate">BTC/USD exchange rate</param>
    /// <param name="network">Blockchain network</param>
    public BitcoinPayment(decimal amount, string walletAddress, decimal exchangeRate, 
                         string network = "Bitcoin Mainnet")
        : base(amount)
    {
        WalletAddress = walletAddress ?? throw new ArgumentNullException(nameof(walletAddress));
        ExchangeRate = exchangeRate;
        Network = network;
        BitcoinAmount = amount / exchangeRate; // Convert USD to BTC
        Confirmations = 0;
        
        Console.WriteLine($"[BitcoinPayment Constructor] Bitcoin payment: {BitcoinAmount:F8} BTC");
        Console.WriteLine($"Exchange rate: 1 BTC = ${exchangeRate:N2} USD");
    }
    
    #endregion
    
    #region Abstract Method Implementations
    
    /// <summary>
    /// Processes the Bitcoin payment.
    /// Completely different from traditional payments - blockchain-based!
    /// Demonstrates polymorphism with decentralized technology.
    /// </summary>
    /// <returns>True if payment initiated successfully</returns>
    public override bool ProcessPayment()
    {
        LogPaymentActivity("Starting Bitcoin payment processing...");
        
        // Step 1: Validate
        if (!ValidatePayment())
        {
            MarkAsFailed("Validation failed");
            return false;
        }
        
        // Step 2: Broadcast transaction to blockchain
        Status = PaymentStatus.Processing;
        Console.WriteLine("\n₿ Processing Bitcoin payment...");
        Console.WriteLine($"💰 Amount: {BitcoinAmount:F8} BTC (${Amount:N2} USD)");
        Console.WriteLine($"📍 Wallet: {MaskedWalletAddress}");
        Console.WriteLine($"🌐 Network: {Network}");
        Console.WriteLine($"⚡ Network fee: {NetworkFeeBTC:F8} BTC");
        
        // Broadcast to network
        if (!BroadcastTransaction())
        {
            MarkAsFailed("Failed to broadcast transaction to blockchain");
            return false;
        }
        
        // Step 3: Wait for confirmations
        Console.WriteLine("\n⏳ Waiting for blockchain confirmations...");
        Console.WriteLine($"Required confirmations: {RequiredConfirmations}");
        
        // Simulate waiting for confirmations
        SimulateConfirmations();
        
        if (IsConfirmed)
        {
            MarkAsCompleted();
            Console.WriteLine($"✅ Payment confirmed on blockchain with {Confirmations} confirmations");
            Console.WriteLine($"Transaction hash: {BlockchainTransactionHash}");
            return true;
        }
        else
        {
            Console.WriteLine($"⚠️ Payment broadcasted but awaiting confirmations ({Confirmations}/{RequiredConfirmations})");
            return false; // Payment initiated but not yet confirmed
        }
    }
    
    /// <summary>
    /// Validates the Bitcoin payment details.
    /// Different validation than traditional payments - blockchain-specific.
    /// </summary>
    /// <returns>True if validation passes</returns>
    public override bool ValidatePayment()
    {
        Console.WriteLine("Validating Bitcoin payment...");
        
        // Validate wallet address format (simplified)
        if (string.IsNullOrWhiteSpace(WalletAddress) || 
            WalletAddress.Length < 26 || 
            WalletAddress.Length > 35)
        {
            Console.WriteLine("❌ Invalid Bitcoin wallet address");
            return false;
        }
        
        // Check if exchange rate is reasonable
        if (ExchangeRate <= 0)
        {
            Console.WriteLine("❌ Invalid exchange rate");
            return false;
        }
        
        // Calculate Bitcoin amount
        if (BitcoinAmount <= 0)
        {
            Console.WriteLine("❌ Invalid Bitcoin amount");
            return false;
        }
        
        Console.WriteLine($"✅ Bitcoin payment validation passed");
        Console.WriteLine($"Converting ${Amount:N2} USD to {BitcoinAmount:F8} BTC");
        
        return true;
    }
    
    /// <summary>
    /// Gets Bitcoin specific payment details.
    /// </summary>
    /// <returns>Bitcoin payment details</returns>
    public override string GetPaymentDetails()
    {
        return $"""
                Bitcoin Payment Details
                =======================
                Blockchain: {Network}
                Wallet Address: {MaskedWalletAddress}
                BTC Amount: {BitcoinAmount:F8} BTC
                USD Amount: ${Amount:N2}
                Exchange Rate: 1 BTC = ${ExchangeRate:N2} USD
                Network Fee: {NetworkFeeBTC:F8} BTC
                Transaction Hash: {BlockchainTransactionHash ?? "Pending"}
                Confirmations: {Confirmations}/{RequiredConfirmations}
                Status: {(IsConfirmed ? "Confirmed ✅" : "Awaiting Confirmations ⏳")}
                """;
    }
    
    #endregion
    
    #region Virtual Method Overrides
    
    /// <summary>
    /// Overrides Refund - Bitcoin transactions are irreversible!
    /// This demonstrates a key difference in blockchain payments.
    /// </summary>
    /// <param name="refundAmount">Amount to refund</param>
    /// <returns>True if refund initiated</returns>
    public override bool Refund(decimal refundAmount)
    {
        Console.WriteLine($"\n₿ Bitcoin refund request...");
        
        if (Status != PaymentStatus.Completed)
        {
            Console.WriteLine("Cannot refund - payment not completed.");
            return false;
        }
        
        // Bitcoin transactions cannot be reversed - must send new transaction
        Console.WriteLine("⚠️ Note: Bitcoin transactions are irreversible");
        Console.WriteLine("Refund requires sending a new Bitcoin transaction");
        
        decimal refundBTC = refundAmount / ExchangeRate;
        
        Console.WriteLine($"\n💰 Initiating refund transaction...");
        Console.WriteLine($"Sending {refundBTC:F8} BTC to {MaskedWalletAddress}");
        Console.WriteLine("Creating new blockchain transaction...");
        
        Thread.Sleep(500);
        
        string refundTxHash = GenerateTransactionHash();
        Console.WriteLine($"Refund transaction broadcasted: {refundTxHash}");
        Console.WriteLine("Waiting for confirmations...");
        
        Status = PaymentStatus.Refunded;
        return true;
    }
    
    /// <summary>
    /// Overrides Cancel - can only cancel if not yet confirmed.
    /// Once confirmed on blockchain, transaction is irreversible.
    /// </summary>
    /// <returns>True if cancellation successful</returns>
    public override bool Cancel()
    {
        Console.WriteLine($"\n₿ Attempting to cancel Bitcoin payment...");
        
        if (IsConfirmed)
        {
            Console.WriteLine("❌ Cannot cancel - transaction confirmed on blockchain");
            Console.WriteLine("Blockchain transactions are irreversible once confirmed");
            Console.WriteLine("Use Refund to send Bitcoin back");
            return false;
        }
        
        if (Confirmations > 0)
        {
            Console.WriteLine("⚠️ Transaction has partial confirmations - cancellation may not succeed");
        }
        
        return base.Cancel();
    }
    
    #endregion
    
    #region Bitcoin Specific Methods
    
    /// <summary>
    /// Broadcasts the transaction to the Bitcoin network.
    /// </summary>
    /// <returns>True if broadcast successful</returns>
    private bool BroadcastTransaction()
    {
        Console.WriteLine("\n📡 Broadcasting transaction to blockchain network...");
        Console.WriteLine("Connecting to Bitcoin nodes...");
        
        Thread.Sleep(400); // Simulate network delay
        
        BlockchainTransactionHash = GenerateTransactionHash();
        
        Console.WriteLine($"✅ Transaction broadcasted successfully");
        Console.WriteLine($"Transaction hash: {BlockchainTransactionHash}");
        Console.WriteLine($"View on blockchain: https://blockchain.info/tx/{BlockchainTransactionHash}");
        
        return true;
    }
    
    /// <summary>
    /// Generates a blockchain transaction hash.
    /// </summary>
    /// <returns>Transaction hash</returns>
    private string GenerateTransactionHash()
    {
        return Guid.NewGuid().ToString("N"); // Simplified - real hashes are SHA-256
    }
    
    /// <summary>
    /// Simulates waiting for blockchain confirmations.
    /// In production, this would monitor the actual blockchain.
    /// </summary>
    private void SimulateConfirmations()
    {
        Console.WriteLine("\n⏳ Monitoring blockchain for confirmations...");
        
        for (int i = 1; i <= RequiredConfirmations; i++)
        {
            Thread.Sleep(500); // Simulate ~10 min per confirmation (simplified)
            Confirmations = i;
            
            Console.WriteLine($"Block #{i} confirmed ✓ ({i}/{RequiredConfirmations})");
            
            if (i == 1)
            {
                Console.WriteLine("First confirmation - payment considered valid but not finalized");
            }
        }
        
        Console.WriteLine($"\n✅ Payment fully confirmed with {Confirmations} confirmations!");
    }
    
    /// <summary>
    /// Checks the current confirmation count on the blockchain.
    /// </summary>
    /// <returns>Current number of confirmations</returns>
    public int CheckConfirmations()
    {
        if (string.IsNullOrEmpty(BlockchainTransactionHash))
        {
            Console.WriteLine("No transaction hash available");
            return 0;
        }
        
        Console.WriteLine($"\n🔍 Checking confirmations for: {BlockchainTransactionHash}");
        Console.WriteLine($"Current confirmations: {Confirmations}");
        Console.WriteLine($"Required confirmations: {RequiredConfirmations}");
        Console.WriteLine($"Status: {(IsConfirmed ? "Confirmed ✅" : "Awaiting confirmations ⏳")}");
        
        return Confirmations;
    }
    
    /// <summary>
    /// Gets the blockchain explorer URL for this transaction.
    /// </summary>
    /// <returns>Blockchain explorer URL</returns>
    public string GetBlockchainExplorerUrl()
    {
        if (string.IsNullOrEmpty(BlockchainTransactionHash))
        {
            return "Transaction not yet broadcasted";
        }
        
        return $"https://blockchain.info/tx/{BlockchainTransactionHash}";
    }
    
    /// <summary>
    /// Converts USD amount to Bitcoin at current exchange rate.
    /// </summary>
    /// <param name="usdAmount">Amount in USD</param>
    /// <returns>Amount in BTC</returns>
    public decimal ConvertToBitcoin(decimal usdAmount)
    {
        return usdAmount / ExchangeRate;
    }
    
    /// <summary>
    /// Converts Bitcoin amount to USD at current exchange rate.
    /// </summary>
    /// <param name="btcAmount">Amount in BTC</param>
    /// <returns>Amount in USD</returns>
    public decimal ConvertToUSD(decimal btcAmount)
    {
        return btcAmount * ExchangeRate;
    }
    
    #endregion
    
    #region Overrides
    
    /// <summary>
    /// Returns a string representation of the Bitcoin payment.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"Bitcoin: {BitcoinAmount:F8} BTC (${Amount:N2} USD) - {Confirmations}/{RequiredConfirmations} confirmations - {Status}";
    }
    
    #endregion
}