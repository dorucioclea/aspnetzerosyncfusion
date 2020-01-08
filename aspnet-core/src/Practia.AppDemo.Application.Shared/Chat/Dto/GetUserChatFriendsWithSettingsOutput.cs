﻿using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Practia.AppDemo.Friendships.Dto;

namespace Practia.AppDemo.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }
        
        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}