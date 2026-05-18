package com.example.articleservice.controller

import com.example.articleservice.dto.*
import com.example.articleservice.service.ArticleService
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api/articles")
class ArticleController(private val articleService: ArticleService) {

    @PostMapping
    fun createArticle(@RequestBody payload: CreateArticleRequest): ResponseEntity<CreateArticleResponse> {
        val response = articleService.createArticle(payload)
        return ResponseEntity.status(201).body(response)
    }

    @GetMapping
    fun getArticles(): ResponseEntity<GetArticlesResponse> {
        val response = articleService.getArticles()
        return ResponseEntity.ok(response)
    }

    @GetMapping("/feed")
    fun feed(): ResponseEntity<FeedResponse> {
        val response = articleService.getFeed()
        return ResponseEntity.ok(response)
    }

    @GetMapping("/{slug}")
    fun getArticle(@PathVariable slug: String): ResponseEntity<GetArticleResponse> {
        val response = articleService.getArticle(slug)
        return ResponseEntity.ok(response)
    }

    @PutMapping("/{slug}")
    fun updateArticle(@PathVariable slug: String, @RequestBody payload: UpdateArticleRequest): ResponseEntity<UpdateArticleResponse> {
        val response = articleService.updateArticle(slug, payload)
        return ResponseEntity.ok(response)
    }

    @DeleteMapping("/{slug}")
    fun deleteArticle(@PathVariable slug: String): ResponseEntity<DeleteArticleResponse> {
        articleService.deleteArticle(slug)
        return ResponseEntity.ok(DeleteArticleResponse(deleted = true))
    }

    @GetMapping("/{slug}/comments")
    fun getComments(@PathVariable slug: String): ResponseEntity<GetCommentsResponse> {
        val response = articleService.getComments(slug)
        return ResponseEntity.ok(response)
    }

    @PostMapping("/{slug}/comments")
    fun addComment(@PathVariable slug: String, @RequestBody payload: AddCommentRequest): ResponseEntity<AddCommentResponse> {
        val response = articleService.addComment(slug, payload)
        return ResponseEntity.status(201).body(response)
    }

    @DeleteMapping("/{slug}/comments/{commentId}")
    fun deleteComment(@PathVariable slug: String, @PathVariable commentId: Long): ResponseEntity<DeleteCommentResponse> {
        articleService.deleteComment(slug, commentId)
        return ResponseEntity.ok(DeleteCommentResponse(status = "deleted"))
    }

    @GetMapping("/{slug}/favorite")
    fun favoriteArticle(@PathVariable slug: String): ResponseEntity<FavoriteArticleResponse> {
        val response = articleService.favoriteArticle(slug)
        return ResponseEntity.ok(response)
    }

    @DeleteMapping("/{slug}/favorite")
    fun unfavoriteArticle(@PathVariable slug: String): ResponseEntity<UnfavoriteArticleResponse> {
        val response = articleService.unfavoriteArticle(slug)
        return ResponseEntity.ok(response)
    }

    @GetMapping("/tags")
    fun getTags(): ResponseEntity<GetTagsResponse> {
        val response = articleService.getTags()
        return ResponseEntity.ok(response)
    }
}
