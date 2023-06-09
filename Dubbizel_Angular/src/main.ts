import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { registerLicense } from '@syncfusion/ej2-base';
import { AppModule } from './app/app.module';
import { enableProdMode } from '@angular/core';

registerLicense('Mgo+DSMBaFt+QHJqVEZrW05FdUBAXWFKblJ8QGdTe11gBShNYlxTR3ZZQ15jSHtWdE1qXnhY;Mgo+DSMBPh8sVXJ1S0R+XVFPcUBDXnxLflF1VWJTelp6cFZWESFaRnZdQV1lSH1Td0BmW3dXeXVT;ORg4AjUWIQA/Gnt2VFhiQlBEfVhdXGBWfFN0RnNYdVt0flFFcC0sT3RfQF5jT39QdkRmXH1XcHRRQA==;MjM5NjU5NEAzMjMxMmUzMDJlMzBRL295cVRGaUhya3pvQStZL1NhNkxvTGdjcU5HKys0WnZYL3V2a1lsM3VjPQ==;MjM5NjU5NUAzMjMxMmUzMDJlMzBlcmZycHF2Mjc2Mmp0SnhWaVVvTjFWSmorWGFPeGNXNFZzejdhMHhEWEcwPQ==;NRAiBiAaIQQuGjN/V0d+Xk9FdlRFQmJKYVF2R2BJflR1cF9CY0wgOX1dQl9gSXhSdUViXHpceXVTRmY=;MjM5NjU5N0AzMjMxMmUzMDJlMzBSekF4TVczY0t4bzloVldwbFNiOXI3eFkvSmY4eGNvR3BEbWVRZmxVOE84PQ==;MjM5NjU5OEAzMjMxMmUzMDJlMzBHSzRhMHZSYWpGOTFnQ0JpcG5naXdvbUpOclM3S0U1ZmMzRzBFU1JUTzQwPQ==;Mgo+DSMBMAY9C3t2VFhiQlBEfVhdXGBWfFN0RnNYdVt0flFFcC0sT3RfQF5jT39QdkRmXH1XcnZVQA==;MjM5NjYwMEAzMjMxMmUzMDJlMzBlZHJjL3daWmttbHlNNFVaK2QraitJZndPcnVTbDZDVzlmblpITUJvTFhRPQ==;MjM5NjYwMUAzMjMxMmUzMDJlMzBIOE4xRCticENRMVdhV2hmTVp4eVZEaWdWWkRUWHRLeFlaaWNkQnRpL1lNPQ==;MjM5NjYwMkAzMjMxMmUzMDJlMzBSekF4TVczY0t4bzloVldwbFNiOXI3eFkvSmY4eGNvR3BEbWVRZmxVOE84PQ==');

// import { environment } from './environments/environment';

// if (environment.production) {
//   enableProdMode();
// }

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));



  // const platform = platformBrowserDynamic();
  // platform.bootstrapModule(AppModule);