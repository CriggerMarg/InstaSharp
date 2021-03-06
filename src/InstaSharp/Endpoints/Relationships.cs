﻿using InstaSharp.Extensions;
using InstaSharp.Models.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstaSharp.Endpoints {

    /// <summary>
    /// The Relationships Endpoint
    /// </summary>
    public class Relationships : InstagramApi {

        /// <summary>
        /// The Action
        /// </summary>
        public enum Action {
            /// <summary>
            /// follow
            /// </summary>
            Follow,
            /// <summary>
            /// unfollow
            /// </summary>
            Unfollow,
            /// <summary>
            /// block
            /// </summary>
            Block,
            /// <summary>
            /// unblock
            /// </summary>
            Unblock,
            /// <summary>
            /// approve
            /// </summary>
            Approve,
            /// <summary>
            /// deny
            /// </summary>
            Deny
        }

        /// <summary>
        /// Relationships Endpoints
        /// </summary>
        /// <param name="config">An instance of the InstagramConfig class.</param>
        public Relationships(InstagramConfig config) : this(config, null)
        {
        }

        /// <summary>
        /// Relationships Endpoints
        /// </summary>
        /// <param name="config">An instance of the InstagramConfig class.</param>
        /// <param name="auth">An instance of the OAuthResponse class.</param>
        public Relationships(InstagramConfig config, OAuthResponse auth) : base("users/", config, auth)
        {
        }

        /// <summary>
        /// Get the list of users this user follows.
        /// <para>Requires Authentication: True</para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        public Task<UsersResponse> Follows()
        {
            AssertIsAuthenticated();

            return Follows(OAuthResponse.User.Id, null);
        }

        /// <summary>
        /// Get the list of users this user follows.
        /// <para>Requires Authentication: False</para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        /// <param name="userId">The list of users that this user id is following.</param>
        public Task<UsersResponse> Follows(int userId)
        {
            return Follows(userId, null);
        }

        /// <summary>
        /// Get the list of users this user follows.
        /// <para>Requires Authentication: False</para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        /// <param name="userId">The list of users that this user id is following.</param>
        /// <param name="cursor">The next cursor id</param>
        public Task<UsersResponse> Follows(int userId, string cursor)
        {
            var request = Request("{id}/follows");
            request.AddUrlSegment("id", userId.ToString());
            request.AddParameter("cursor", cursor);

            return Client.ExecuteAsync<UsersResponse>(request);
        }

        /// <summary>
        /// Get the list of users this user is followed by.
        /// <para>Requires Authentication: True</para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        public Task<UsersResponse> FollowedBy()
        {
            AssertIsAuthenticated();

            return FollowedBy(OAuthResponse.User.Id, null);
        }

        /// <summary>
        /// Get the list of users this user is followed by.
        /// <para>Requires Authentication: False</para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        /// <param name="userId">The id of the user to get the followers of.</param>
        public Task<UsersResponse> FollowedBy(int userId)
        {
            return FollowedBy(userId, null);
        }

        /// <summary>
        /// Get the list of users this user is followed by.
        /// <para>Requires Authentication: False</para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        /// <param name="userId">The id of the user to get the followers of.</param>
        /// <param name="cursor">The next cursor id</param>
        public Task<UsersResponse> FollowedBy(int userId, string cursor)
        {
            var request = Request("{id}/followed-by");
            request.AddUrlSegment("id", userId.ToString());
            request.AddParameter("cursor", cursor);
            
            return Client.ExecuteAsync<UsersResponse>(request);
        }

        /// <summary>
        /// List the users who have requested this user's permission to follow.
        /// <para>
        /// <c>Requires Authentication:</c> True
        /// </para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        public Task<UsersResponse> RequestedBy()
        {
            var request = Request("self/requested-by");
            return Client.ExecuteAsync<UsersResponse>(request);
        }

        /// <summary>
        /// Get information about a relationship to another user.
        /// <para>
        /// <c>Requires Authentication:</c> True
        /// </para>
        /// </summary>
        public Task<RelationshipResponse> Relationship(int userId)
        {
            var request = Request("{id}/relationship");
            request.AddUrlSegment("id", userId.ToString());

            return Client.ExecuteAsync<RelationshipResponse>(request);
        }

        /// <summary>
        /// Modify the relationship between the current user and the target user.
        /// <para>
        /// <c>Requires Authentication:</c> True
        /// </para>
        /// <para>
        /// <c>Required scope:</c> relationships
        /// </para>
        /// </summary>
        /// <param name="userId">The user id about which to get relationship information.</param>
        /// <param name="action">One of Action enum.</param>
        public Task<RelationshipResponse> Relationship(int userId, Action action)
        {
            var request = Request("{id}/relationship", HttpMethod.Post);
            request.AddUrlSegment("id", userId.ToString());
            request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() {new KeyValuePair<string, string>("action", action.ToString().ToLower())});
            return Client.ExecuteAsync<RelationshipResponse>(request);
        }
    }
}
