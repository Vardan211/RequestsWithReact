<link rel="stylesheet" href="css/bootstrap.min.css" />
<link rel="stylesheet" href="css/main.css" />

<script
  type="text/javascript"
  src="//ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js"
></script>
<script
  type="text/javascript"
  charset="utf-8"
  src="https://portal.osinit.com/_layouts/15/sp.runtime.js"
></script>
<script
  type="text/javascript"
  charset="utf-8"
  src="https://portal.osinit.com/_layouts/15/sp.js"
></script>

<div id="root"></div>

<script>
  function getCurrentUser(func) {
    const clientContext = new SP.ClientContext("https://portal.osinit.com/");
    var web = clientContext.get_web();
    var user = web.get_currentUser();
    clientContext.load(user);
    clientContext.executeQueryAsync(() => {
      console.log(user);
      console.log(user.get_title());
      console.log(user.get_email());
      func([user.get_email(), user.get_title()]);
    });
  }
</script>
<script
  defer="defer"
  src="/dotnet/SiteAssets/build/static/js/main.production.js"
></script>
<link
  href="/dotnet/SiteAssets/build/static/css/main.production.css"
  rel="stylesheet"
/>
<link rel="apple-touch-icon" href="/dotnet/SiteAssets/build/logo192.png" />
<link rel="manifest" href="/dotnet/SiteAssets/build/manifest.json" />
