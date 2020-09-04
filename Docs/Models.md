# Models description

## Table of content
> [Template](#template)\
> [Media Raw](#media-raw)\
> [Message Media](#message-media)\
> [Theme Media](#theme-media)\
> [Photo](#photo)\
> [User](#user)\
> [Theme](#theme)\
> [Message](#message)

### 'Template'
'Short description'
#### Fields 
- 'Field Name 1' - 'Field 1 description'
- 'Field Name 2' - 'Field 2 description'

### Media Raw
Raw media data to link with media types
#### Fields
- value - stored bytes
- preview - small size preview (small image or video preview), only for images and video
- name - name.extension (e.g. "empty.jpg")
- type - html media type (e.g. "image\png")

### Message Media
Model for media in messages
#### Fields
- media - [Media Raw](#media-raw)  

### Theme Media
Model for media in theme header
#### Fields
- media - [Media Raw](#media-raw)

### Photo
Model for user photo
#### Fields
- photo - [Media Raw](#media-raw) 

### User
Model for user
#### Fields
- name - name visible for other user
- photo - [Photo](#photo) 

### Theme
Model for theme
#### Fields
- name - theme name
- description - theme header content
- media - list of [Theme Media](#theme-media)
- messages - list of [Message](#message) 
- author - [User](#user)

### Message
Model for message
#### Fields
- content - message content
- media - list of [Message Media](#message-media) 
- author - [User](#user)
