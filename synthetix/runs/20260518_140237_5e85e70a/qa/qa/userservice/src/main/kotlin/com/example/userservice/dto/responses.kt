package com.example.userservice.dto

import java.util.UUID

data class SignupResponse(
    val user_id: UUID,
    val username: String,
    val created_at: String
)

data class JwtAuthResponse(
    val token: String,
    val token_type: String,
    val username: String,
    val authorities: List<String>,
    val expires_at: String
)

data class UserListResponse(
    val items: List<String>,
    val total: Int,
    val page: Int,
    val page_size: Int
)

data class UpdateUserResponse(
    val status: String,
    val entity_id: UUID
)

data class ProfileResponse(
    val id: UUID,
    val created_at: String,
    val updated_at: String
)

data class FollowResponse(
    val status: String,
    val entity_id: UUID
)

data class UnfollowResponse(
    val status: String
)