Feature: PostProfile
	Test POST operation using REST-assured library

@smoke
Scenario: Verify Post operation for Profile
	Given I perform POST operation for "posts/postId/profile" with body
	| Key     | Value   |
	| name    | Sams    |
	| postId | 2    |
	Then I should see the "name" name as "Sams"
