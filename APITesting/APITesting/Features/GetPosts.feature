Feature: GetPosts
	Test GET posts operation with restshar.net

@mytag
Scenario: VarifyAuthorOfThePosts
	Given I perfom GET operation for "posts/{postid}"
	And I perform operation for post "1"
	Then I should see the "author" name as "typicode"
