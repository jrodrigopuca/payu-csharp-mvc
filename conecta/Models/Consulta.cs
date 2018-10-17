using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace conecta.Models
{
    public class DataAcceso
    {
        public string apiLogin;
        public string apiKey;
    }

    public class Details
    {
        public int orderId { get; set; }
    }

    public class CreditCardToken
    {
        public int payerId { get; set; }
        public string name { get; set; }
        public int identificationNumber { get; set; }
        public string paymentMethod { get; set; }
        public string number { get; set; }
        public string expirationDate { get; set; }
        public string creationDate { get; set; }
        public string maskedNumber { get; set; }
        public string errorDescription { get; set; }
        public string creditCardTokenId { get; set; }
    }

    public class CreditCard
    {
        public string securityCode { get; set; }
    }

    public class ExtraParameters
    {
        public int INSTALLMENTS_NUMBER { get; set; }
    }

    public class TXVALUE
    {
        public int value { get; set; }
        public string currency { get; set; }
    }

    public class AdditionalValues
    {
        public TXVALUE TX_VALUE { get; set; }
    }

    public class Actor
    {
        public string fullName { get; set; }
        public string emailAddress { get; set; }
        public string contactPhone { get; set; }
        public string dniNumber { get; set; }
    }

    public class Address
    {
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
    }


    public class Payer:Actor
    {
        public string merchantPayerId { get; set; }
        public Address billingAddress { get; set; }
    }

    public class Buyer:Actor
    {
        public int merchantBuyerId { get; set; }
        public Address shippingAddress { get; set; }
    }

    public class Order
    {
        public int accountId { get; set; }
        public string referenceCode { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string signature { get; set; }
        public string notifyUrl { get; set; }
        public AdditionalValues additionalValues { get; set; }
        public Buyer buyer { get; set; }
        //public ShippingAddress shippingAddress { get; set; }
    }

    public class Transaction
    {
        public Order order { get; set; }
        public Payer payer { get; set; }
        public string creditCardTokenId { get; set; }
        public CreditCard creditCard { get; set; }
        public ExtraParameters extraParameters { get; set; }
        public string type { get; set; }
        public string paymentMethod { get; set; }
        public string paymentCountry { get; set; }
        public string deviceSessionId { get; set; }
        public string ipAddress { get; set; }
        public string cookie { get; set; }
        public string userAgent { get; set; }
    }

    public class Solicitud
    {
        public bool test { get; set; }
        public string language { get; set; }
        public string command { get; set; }
        public DataAcceso merchant { get; set; }
        public Details details { get; set; }
        public CreditCardToken creditCardToken { get; set; }
        public Transaction transaction { get; set; }
    }


    public class TransactionResponse
    {
        public int orderId { get; set; }
        public string transactionId { get; set; }
        public string state { get; set; }
        public object paymentNetworkResponseCode { get; set; }
        public object paymentNetworkResponseErrorMessage { get; set; }
        public object trazabilityCode { get; set; }
        public object authorizationCode { get; set; }
        public object pendingReason { get; set; }
        public string responseCode { get; set; }
        public object errorCode { get; set; }
        public object responseMessage { get; set; }
        public object transactionDate { get; set; }
        public object transactionTime { get; set; }
        public object operationDate { get; set; }
        public object extraParameters { get; set; }
    }


    public class Result
    {
         public string payload { get; set; }
    }
    public class Respuesta
    {
        public string code { get; set; }
        public string error { get; set; }
        public Result result { get; set; }
        public CreditCardToken creditCardToken { get; set; }
        public TransactionResponse transactionResponse { get; set; }
    }


}
 