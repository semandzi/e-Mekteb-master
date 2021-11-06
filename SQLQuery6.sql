SELECT ImeiPrezime,  COUNT(ImeiPrezime)
FROM AspNetUsers
GROUP BY ImeiPrezime
Having COUNT(ImeiPrezime) > 1