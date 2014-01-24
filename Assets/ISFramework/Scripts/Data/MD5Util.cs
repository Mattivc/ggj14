using System;

public static class MD5Util
{
	public static string Md5Sum (string strToHash)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding ();
		byte[] bytes = ue.GetBytes (strToHash);
		
		return Md5Sum(bytes);
	}
	
	public static string Md5Sum (byte[] dataToHash)
	{
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider ();
		byte[] hashBytes = md5.ComputeHash (dataToHash);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString (hashBytes[i], 16).PadLeft (2, '0');
		}
		
		return hashString.PadLeft (32, '0');
	}
	
	
}