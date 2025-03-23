using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project12_JwtToken.JWT
{
    public class TokenGenerator
    {
        public string GenerateJwtToken(string username,string email,string name,string surname)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT"));//Güvenlik Anahtarı
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);//kimlik bilgisi ve kullanılacak algoritma

            var claimsExample = new[]//istekte buluınacak token kişisi
            {
                new Claim(JwtRegisteredClaimNames.Sub,username), //Sub Id için Kullanılan Parametre
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.GivenName,name),//GivenName =>ad
                new Claim(JwtRegisteredClaimNames.FamilyName,surname),//FamilyName=>Soyad
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),//benzersiz bir guid oluştur;
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",//tokenın yayınlayıcı web tabanlı uygulamlarda locahost olur genelde
                audience: "localhost",//dinleyici
                claims: claimsExample,
                expires: DateTime.Now.AddMinutes(5),//Tokenin süresi şuan 5 dk
                signingCredentials: credentials);//Security ve algoritmasıı tutucak 

            return new JwtSecurityTokenHandler().WriteToken(token);//tokenden gelen değeri döner



        }

        public string GenerateJwtToken2(string username)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT"));//Güvenlik Anahtarı
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);//kimlik bilgisi ve kullanılacak algoritma

            var claimsExample = new[]//istekte buluınacak token kişisi
            {
                new Claim(JwtRegisteredClaimNames.Sub,username), //Sub Id için Kullanılan Parametre
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),//benzersiz bir guid oluştur;
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",//tokenın yayınlayıcı web tabanlı uygulamlarda locahost olur genelde
                audience: "localhost",//dinleyici
                claims: claimsExample,
                expires: DateTime.Now.AddMinutes(5),//Tokenin süresi şuan 5 dk
                signingCredentials: credentials);//Security ve algoritmasıı tutucak 

            return new JwtSecurityTokenHandler().WriteToken(token);//tokenden gelen değeri döner



        }
    }
}
