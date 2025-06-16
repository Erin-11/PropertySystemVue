using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace KnightFrank.AuthServer
{
    public static class X509
    {
        public static X509Certificate2 GetCertificate(string thumbprint)
        {
            thumbprint = Regex.Replace(thumbprint, @"[^\da-fA-F]", string.Empty).ToUpper();
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            try
            {
                store.Open(OpenFlags.ReadOnly);

                var certCollection = store.Certificates;
                var signingCert = certCollection.Find(X509FindType.FindByThumbprint, thumbprint, false);
                if (signingCert.Count == 0)
                {
                    throw new FileNotFoundException(string.Format("Cert with thumbprint: '{0}' not found in local machine cert store.", thumbprint));
                }

                return signingCert[0];
            }
            catch
            {
                throw new FileNotFoundException(string.Format("Cert with thumbprint: '{0}' not found in local machine cert store.", thumbprint));
            }
            finally
            {
                store.Close();
            }
            //string _thumbprint = thumbprint.ToUpper();
            //var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //store.Open(OpenFlags.ReadOnly);
            //var certCollection = store.Certificates;
            //var certThumbprint = certCollection[0].Thumbprint;

            //var currentCerts = certCollection.Find(X509FindType.FindByThumbprint, _thumbprint, false);
            //return currentCerts.Count == 0 ? null : currentCerts[0];

            //using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            //{
            //    // D2BF1338E66912B58BB008780D51D0398F16D2BC
            //    certStore.Open(OpenFlags.ReadOnly);
            //    X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, (string)thumbprint, false);
            //    if (certCollection.Count > 0) return certCollection[0];
            //}
            //return null;
        }
    }
}
