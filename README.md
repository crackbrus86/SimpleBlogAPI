# SimpleBlogAPI

## User

## Login

* **URL** `/api/user/authenticate`
* **Method** `POST`
* **Authentication** `none`
* **Request:**

Field | Type | Note
------|------|------
username | string | required
password | string | required

* **Response:**

Field | Type
------|------
token | string
username | string
email | string
profile | [Profile](#profile)

* **Profile:**

Field | Type
------|------
id | number
firstName | string
lastName | string

## Register

* **URL** `/api/user/register`
* **Method** `POST`
* **Authentication** `none`
* **Request:**

Field | Type | Note
------|------|------
username | string | required
email | string | required
firstName | string | required
lastName | string | required
password | string | required
confirm | string | required, match to password

* **Response:**

Field | Type
------|------
message | string

## Profile

## Get Profile

* **URL:** `/api/profile/getprofile`
* **Method:** `GET`
* **Authentication** `Bearer Token`
* **Request:**

Field | Type | Note
------|------|------
id | number | required

* **Response:**

Field | Type
------|------
id | number
firstName | string
lastName | string
phone | string
photo | string

## Save

* **URL:** `/api/profile/save`
* **Method:** `POST`
* **Authentication** `Bearer Token`
* **Request:**

Field | Type | Note
------|------|------
id | number | requiered
firstName | string | required
lastName | string | required
phone | string | nullable
photo | string (base64) | nullable

* **Response:**

Field | Type
------|------
message | string