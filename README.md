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
id | number | required
firstName | string | required
lastName | string | required
phone | string | nullable
photo | string (base64) | nullable

* **Response:**

Field | Type
------|------
message | string

## Post

## Get All

* **URL:** `/api/post/getall`
* **Method:** `GET`
* **Authentication** `none`
* **Request:** `none`

* **Response:**

Field | Type
------|------
id | number
title | string
content | string
publishedDate | Date

## Get My Posts

* **URL:** `/api/post/getmyposts`
* **Method:** `GET`
* **Authentication** `Bearer Token`
* **Request:** `none`

* **Response:**

Field | Type
------|------
id | number
title | string
content | string
createdDate | Date
updatedDate | Date
isPublished | boolean
publishedDate | Date

## Get Post

* **URL:** `/api/post/getpost?id=:postId`
* **Method:** `GET`
* **Authentication** `none`
* **Request:** `none`

* **Response:**

Field | Type
------|------
id | number
title | string
content | string
publishedDate | Date
author | string

## Get My Post

* **URL:** `/api/post/getmypost?id=:postId`
* **Method:** `GET`
* **Authentication** `Bearer Token`
* **Request:** `none`

* **Response:**

Field | Type
------|------
id | number
title | string
content | string
createdDate | Date
updatedDate | Date
isPublished | boolean
publishedDate | Date

## Create

* **URL:** `/api/post/create`
* **Method:** `POST`
* **Authentication** `Bearer Token`
* **Request:**

Field | Type | Note
------|------|------
title | string | required
content | string | required

* **Response:**

Field | Type
------|------
message | string

## Update

* **URL:** `/api/post/update`
* **Method:** `POST`
* **Authentication** `Bearer Token`
* **Request:**

Field | Type | Note
------|------|------
id | number | required
title | string | required
content | string | required
createdDate | Date | nullable
updatedDate | Date | nullable
isPublished | boolean | nullable
publishedDate | Date | nullable

* **Response:**

Field | Type
------|------
message | string

## Publish

* **URL:** `/api/post/publish`
* **Method:** `POST`
* **Authentication** `Bearer Token`
* **Request:**

Field | Type | Note
------|------|------
id | number | required

* **Response:**

Field | Type
------|------
message | string

## Delete

* **URL:** `/api/post/delete`
* **Method:** `POST`
* **Authentication** `Bearer Token`
* **Request:**

Field | Type | Note
------|------|------
id | number | required

* **Response:**

Field | Type
------|------
message | string
