package com.example.commentservice.controller

import com.example.commentservice.model.CommentRequest
import com.example.commentservice.model.CommentResponse
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api/commentservice")
class CommentController {

    @PostMapping("/commentview")
    fun commentView(@RequestBody request: CommentRequest): ResponseEntity<CommentResponse> {
        // Simulate processing logic
        val response = CommentResponse(status = "success", id = "1")
        return ResponseEntity.status(201).body(response)
    }

    @PostMapping("/createcommentrequest")
    fun createCommentRequest(@RequestBody request: CommentRequest): ResponseEntity<CommentResponse> {
        // Simulate processing logic
        val response = CommentResponse(status = "success", id = "2")
        return ResponseEntity.status(201).body(response)
    }

    @PostMapping("/tagrepository")
    fun tagRepository(@RequestBody request: CommentRequest): ResponseEntity<CommentResponse> {
        // Simulate processing logic
        val response = CommentResponse(status = "success", id = "3")
        return ResponseEntity.status(201).body(response)
    }

    @PostMapping("/globalcontrolleradvice")
    fun globalControllerAdvice(@RequestBody request: CommentRequest): ResponseEntity<CommentResponse> {
        // Simulate processing logic
        val response = CommentResponse(status = "success", id = "4")
        return ResponseEntity.status(201).body(response)
    }
}