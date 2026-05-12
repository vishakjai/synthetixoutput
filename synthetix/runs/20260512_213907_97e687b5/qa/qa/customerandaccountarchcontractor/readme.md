# Customer and Account Archcontractor API

This API is part of the MagicBox Modernization Architecture and provides endpoints for managing customer and account data.

## Building and Running

To build and run the application locally, use the following commands:

```bash
# Build the Docker image
docker build -t customer-and-account-archcontractor-api .

# Run the Docker container
docker run -p 8080:8080 customer-and-account-archcontractor-api
```

## Endpoints

- Health: `GET /health`
- Ready: `GET /ready`
- Contractor Placement Entity Controller: `GET /contractorplacement/contractorplacemententitycontroller`
- Contractor Placement Search Controller: `GET /contractorplacement/contractorplacementsearchcontroller`
- Customer Profile: `GET /customer/profile`
- Customer Account List Entity Controller: `GET /customer/accountlistentitycontroller`
- Customer Account List Search Controller: `GET /customer/accountlistsearchcontroller`
- Customer Account Search Controller: `GET /customer/accountsearchcontroller`
- Customer Contact Search Controller: `GET /customer/contactsearchcontroller`
- Customer Job Listing Search Controller: `GET /customer/joblistingsearchcontroller`
- Customer Training Search Controller: `GET /customer/trainingsearchcontroller`
- Customer VMS Portal Search Controller: `GET /customer/vmsportalsearchcontroller`
- Onboarding API Controller: `GET /onboarding/apicontroller`
- Onboarding Demographic Search Controller: `GET /onboarding/onbdemographicsearchcontroller`
- Platform Account Entity Controller: `GET /platform/accountentitycontroller`
- Platform Address Entity Controller: `POST /platform/addressentitycontroller`
- Platform Cross Selling Lead Search Controller: `GET /platform/crosssellingleadsearchcontroller`
- Platform End Client Entity Controller: `GET /platform/endcliententitycontroller`
- Platform Search Controller: `GET /platform/searchcontroller`
- Platform TS Overdue Entity Controller: `GET /platform/tsoverdueentitycontroller`
- Reporting On-Demand Timesheets Search Controller: `GET /reporting/ondemandtimesheetssearchcontroller`
- Reporting Ticker Search Controller: `GET /reporting/tickersearchcontroller`
- Reporting TS Daily Search Controller: `GET /reporting/tsdailysearchcontroller`
- Reporting TS On-Demand Transfer Details Search Controller: `GET /reporting/tsondemandtransferdetailssearchcontroller`
- Reporting TS Update Search Controller: `PUT /reporting/tsupdatesearchcontroller`
- Shared Utilities Address Search Controller: `POST /sharedutilities/addresssearchcontroller`
- Shared Utilities Addrmaster Entity Controller: `POST /sharedutilities/addrmasterentitycontroller`
- Shared Utilities Addrmaster Search Controller: `POST /sharedutilities/addrmastersearchcontroller`
- Shared Utilities Archcontractor Entity Controller: `GET /sharedutilities/archcontractorentitycontroller`
- Shared Utilities Archcontractor Search Controller: `GET /sharedutilities/archcontractorsearchcontroller`
- Shared Utilities Asset Search Controller: `GET /sharedutilities/assetsearchcontroller`
- Shared Utilities Auditor Search Controller: `GET /sharedutilities/auditorsearchcontroller`
- Shared Utilities Backup HR A Search Controller: `GET /sharedutilities/backuphrasearchcontroller`
