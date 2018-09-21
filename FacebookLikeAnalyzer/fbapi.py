import urllib3
import requests
import facebook
import json
import operator

#r = requests.get(url = "https://graph.facebook.com/820882001277849")

token="EAAD2ceoqKHEBAOJmm8HBKMcChq8PBtLEH2mcm0AbmfGp9DKRZBNs3R5y0QRS3NKHvmav5Rct0ngK4mTfIeRUebZBr2NcxnK50F1bYMj29Fjm8XUfNwb2kvm2UybbzOdBQokaTBqwUZC8wc2JnQaiV7whqVCJGrxanBsp6fUewZDZD"

'''
Note: We can't sort a dictionary straight up, we prob have to convert it into a list then sort it
so it's gonna be a slight mission. And also we have to figure out the paging of the results, right
now we only get the first page of users posts. Still lit tho.
'''

#sort page in list form
def sortPage (page):
    return sorted(page,key=lambda x: x[3], reverse=True)

#turns the page of posts into a list instead
def pageToList (page):
    posts = []
    for post in page:
        posts = posts + [(str(post["id"]),
                post["created_time"],
                (post["message"] if ("message" in post) else "--  no message --"),
                ((((post)["likes"])["summary"])["total_count"]))]
    return posts

#output in a decent looking way
def cleanPrintList (page):
    for post in page:
        print ("Post ID: " + post[0] + "\n" +
               "Created On: " + post[1] + "\n" +
               "Message: " + post[2] + "\n" +
               "Likes: " + str(post[3]) + "\n" +
               "____________________________________________________" + "\n")

def returnResults ():
    graph = facebook.GraphAPI(access_token=token, version = 2.7)
    posts = graph.request('me/posts?fields=created_time,message,likes.summary(true)&limit=999')

    #gets first page
    paging = posts["paging"]
    currentPage = posts["data"]

    allPosts = pageToList(currentPage)

    while (currentPage != []):
        currentPage = requests.get((paging["next"])).json()
        for post in pageToList(currentPage["data"]):
            allPosts += [post]
        if ("paging" in currentPage):
            paging = currentPage["paging"]
        else:
            break

    return (sortPage(allPosts))

def main ():
    cleanPrintList(returnResults())



if __name__ == "__main__":
    main()
