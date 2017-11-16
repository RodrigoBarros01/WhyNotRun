using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models
{
    public class Token
    {
        private string GenerateSecret(string id, string email)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes($"{DateTime.Now.Day.ToString() + id + email}");
            string secret = Convert.ToBase64String(b);
            return secret + "RTZljcvklaKKKJ";
        }


        public string GenerateToken(string id, string email)
        {
            var payload = new Dictionary<string, object>
            {
                { "id", id },
                { "email", email }
            };

            var secret = GenerateSecret(id, email);


            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }

        public string DecodeToken(string token, string secret)
        {
            
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);
                return json;
            }
            catch (TokenExpiredException)
            {
                return "Token has expired";
            }
            catch (SignatureVerificationException)
            {
                return "Token has invalid signature";
            }
        }

    }
}