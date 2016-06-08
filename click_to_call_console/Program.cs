using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSG360API;
using System.Net;

namespace click_to_call_console
{
    class Program
    {
        static Msg360APIRestClient Client;
        public const string AuthToken = "1acff5853c8f26d269b259b79db3ebb1";   // Your Token
        public const string AccountSid = "5fd7e139-080c-49ab-bbe4-7c9f686dc64e";  // Your AccountSID
        public const string ResponseFormat = ".xml";
        public const string BaseUrl = "https://api.message360.com/api/";

        static void Main(string[] args)
        {


             Client = new Msg360APIRestClient(AccountSid, AuthToken, ResponseFormat, BaseUrl); // Set the client object for authentication to the REST API



            string strCRMUserID = "5005"; // Fake Insellerate Sales Agent UserID
            string strSalesNumber = "9493518878"; // Sales Agent Phone Number
            string strCustomerNumber = "8003824913"; // Customer Phone Number
            string strMessage360Number = "3234576819"; // Message360 owned Phone Number or confirmed number

            //Set outbound call options
            CallOptions co = new CallOptions();
            string strUrl = "https://customapps.message360.com/test/inselleratedial.php?phone=" + strMessage360Number + "&customer=" + strCustomerNumber + "&name=John";  // The URL that will load after the call in answered, this will contain my InboundXML that has my <dial> tag, I just added in a static name
            /*

            On the page that the url posts to I will get the below data:

            carrier	LEVEL 3 COMMUNICATIONS, LLC
            wireless	false
            zipcode	92675
            city	Capistrano Valley
            CallSid	7ce6be7a-c1ed-e285-69ad-xyz
            AccountSid	abc-080c-49ab-bbe4-xyz
            From	3234576819
            To	9496827724
            phone	3234576819
            customer	8003824913
            name	John

            My output at that url is:
                        
            <Response>
            <Play>http://customapps.message360.com/ytel_lead/trans.wav</Play>
            <Say>Please hold while I make a call out to, John</Say>
            <Dial tocountrycode="1" fromcountrycode="1" callerId="3234576819">8003824913</Dial>
            </Response>

            PHP behind the page:

            <Response>
            <Play>http://customapps.message360.com/ytel_lead/trans.wav</Play>
            <Say>Please hold while I make a call out to, <?php echo $_GET['name'] ?></Say>
            <Dial tocountrycode="1" fromcountrycode="1" callerId="<?php echo $_GET['phone'] ?>"><?php echo $_GET['customer'] ?></Dial>
            </Response>

            */

            Console.WriteLine(strUrl);
            string strTo = strSalesNumber;  // Sales Agent I am going to call
            string strFrom = strMessage360Number; // The message360 number that I will be the calling the sales agent and the customer from
            int intToCountryCode = 1;   // Countrycode of the number being dialed.  North american numbers supported for now only
            int intFromCountryCode = 1; // Countrycode of the number dialing from.  North american numbers supported for now only




            string xmlResponse = Client.Calls_MakeCall(strFrom, strTo, strUrl, intFromCountryCode, intToCountryCode);  // Make the call
            Console.WriteLine(xmlResponse);
            /*

             Parse this xml (or JSON) and put the values into a DB table where you log calls with the CallSID being the primary key
                a. Add AccountSID, strCRMUserID to this table as additional indexes
                or
                b. Insert AccountSID, strCRMUserID and CallSID into another table and join the tables when needed.
            
            */
            Console.ReadLine();

        }
    }
}
