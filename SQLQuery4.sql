Set IDENTITY_INSERT [Comment] ON
INSERT INTO Comment (Id,PostId,UserProfileId, Subject,Content, CreateDateTime) VALUES (1, 1,1,'test','testing', '2023/06/29'); Set IDENTITY_INSERT [Comment] OFF
