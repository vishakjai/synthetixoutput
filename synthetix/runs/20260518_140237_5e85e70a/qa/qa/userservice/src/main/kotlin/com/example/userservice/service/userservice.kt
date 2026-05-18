package com.example.userservice.service

import com.example.userservice.dto.*
import org.springframework.stereotype.Service
import java.util.UUID

@Service
class UserService {

    fun signup(request: SignupRequest): SignupResponse {
        // Simulate user creation
        return SignupResponse(
            user_id = UUID.randomUUID(),
            username = request.username,
            created_at = "2023-10-01T00:00:00Z"
        )
    }

    fun login(request: LoginRequest): JwtAuthResponse {
        // Simulate login
        return JwtAuthResponse(
            token = "dummy-token",
            token_type = "Bearer",
            username = request.username,
            authorities = listOf("ROLE_USER"),
            expires_at = "2023-10-02T00:00:00Z"
        )
    }

    fun getCurrentUser(): UserListResponse {
        // Simulate fetching current user
        return UserListResponse(
            items = listOf(),
            total = 0,
            page = 1,
            page_size = 10
        )
    }

    fun updateUser(request: UpdateUserRequest): UpdateUserResponse {
        // Simulate user update
        return UpdateUserResponse(
            status = "updated",
            entity_id = UUID.randomUUID()
        )
    }

    fun getProfile(username: String): ProfileResponse {
        // Simulate fetching profile
        return ProfileResponse(
            id = UUID.randomUUID(),
            created_at = "2023-10-01T00:00:00Z",
            updated_at = "2023-10-01T00:00:00Z"
        )
    }

    fun follow(username: String, request: FollowRequest): FollowResponse {
        // Simulate follow
        return FollowResponse(
            status = "followed",
            entity_id = UUID.randomUUID()
        )
    }

    fun unfollow(username: String): UnfollowResponse {
        // Simulate unfollow
        return UnfollowResponse(
            status = "unfollowed"
        )
    }
}